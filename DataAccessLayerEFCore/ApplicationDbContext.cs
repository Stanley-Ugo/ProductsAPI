using Microsoft.EntityFrameworkCore;
using ProductsAPI.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccessLayerEFCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        //this seeds the initial data for admin
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User {Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = "admin", Role = "Admin"});
        }
    }
}
