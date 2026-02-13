using BackendEcommerchSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<User> users { get; set; }  
        public DbSet<Category> categories { get; set; } 
       public DbSet<SubCategory> subcategories { get; set; }        
        public DbSet<Product> products { get;   set; }  
        
        public DbSet<Order> orders { get; set; }    
        public DbSet<ProductImage> ProductImages { get; set; }          
         public DbSet<Brand> rands { get; set; }        
        public DbSet<OrderItem> OrderItems { get; set; }        
    }
}

