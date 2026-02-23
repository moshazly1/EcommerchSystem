using BackendEcommerchSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 5,
                FullName = "System Admin",
                Email = "admin@myshop.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mohamed@2005"),
            Role = Enums.UserRole.Admin , 
             CreatedAt = DateTime.Now,  
             IsAcive = true     
            }); 
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

