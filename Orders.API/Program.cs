using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Orders.Data.Models;
using Orders.Infrastructure.AutoMapper;
using Orders.Infrastructure.Extinction;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Orders.Core.Options;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<OrdersDbContext>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supportedCultures = opts.SupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("en"),
        new CultureInfo("ar-SA"),
        new CultureInfo("ar")
    };
    opts.DefaultRequestCulture = new RequestCulture("en-US");
    opts.SupportedCultures = new List<CultureInfo>{
        new CultureInfo("en-US"),
        new CultureInfo("en")
    };
    opts.SupportedUICultures = supportedCultures;
    opts.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var defaultLang = "en-US";
        var userLanguges = context.Request.GetTypedHeaders().AcceptLanguage;
        if (userLanguges.Any() && !string.IsNullOrWhiteSpace(userLanguges.FirstOrDefault()?.ToString()))
        {
            var passedLanguage = userLanguges.FirstOrDefault()?.ToString();
            if (!string.IsNullOrWhiteSpace(passedLanguage) && passedLanguage.StartsWith("ar", StringComparison.OrdinalIgnoreCase))
            {
                defaultLang = "ar-SA";
            }
        }
        return Task.FromResult(new ProviderCultureResult(defaultLang));
    }));

});
builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Register custom services
builder.Services.RegisterServices();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
    };
});

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            // Use the custom contract resolver
            options.SerializerSettings.ContractResolver = new IgnorePropertiesResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

var app = builder.Build();
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();