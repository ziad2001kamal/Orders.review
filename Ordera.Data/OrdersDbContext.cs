
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

    public class OrdersDbContext : IdentityDbContext<User>
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) 
            : base(options)
        {
        
        }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<OrderItem>().HasKey(x => new {x.OrderId, x.MealId});
       // builder.Entity<BaseEntity>().HasQueryFilter(x => !x.IsDelete);
        builder.Entity<User>().HasQueryFilter(x => !x.IsDelete);

        //HasQueryFilter
    }
    public DbSet<Resturent> Resturents { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

}


