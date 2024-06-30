using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orders.Core.Dtos;
using Orders.Core.Exceptions;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Orders.API.Constant;
using Microsoft.Extensions.Options;
using Orders.Core.ViewModels;
using AutoMapper;
using Orders.Core.Options;
namespace Orders.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;
        private readonly JwtOptions _options;


        public AuthService(UserManager<User> userManager, OrdersDbContext db , IMapper mapper , IOptions<JwtOptions> options)
        {
            _userManager = userManager;
            _db = db;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<LoginResponseViewModel> Login(LoginDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == dto.UserName);
            if (user == null)
            {
                throw new InvalidUsernameOrPassword();
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
            {
                throw new InvalidUsernameOrPassword();
            }
            var response = new LoginResponseViewModel();
            response.AccessToken = await GenerateAccessToken(user);
            response.User = _mapper.Map<UserViewModel>(user);
            return response;
        }
        private async Task<AccessTokenViewModel> GenerateAccessToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(Claims.PhoneNumber, user.PhoneNumber),
            new Claim(Claims.UserId,user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMonths(1);
            var accessToken = new JwtSecurityToken(
                issuer: "https://localhost:44388/",
                audience: "https://localhost:44388/",
                claims: claims,
                expires: expires,
                signingCredentials: credentials
);
            var result = new AccessTokenViewModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                ExpireAt = expires
            };
            return result;
        }
        //string username, string password
    }
}