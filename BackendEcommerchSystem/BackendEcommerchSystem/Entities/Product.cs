using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BackendEcommerchSystem.Entities
{
    public class Product
    { 

        [Key]   
        public  int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]  
        public decimal Price { get; set; }      

        [Required]  
        public int Stock {  get; set; }

        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")] 
        public Category?Category { get; set;  }

        public bool IsActive { get; set; } = true; 

        public DateTime CreatedAt { get; set; } = DateTime.Now; 

        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItems { get; set; }  = new  List<OrderItem>(); 


    }
}
