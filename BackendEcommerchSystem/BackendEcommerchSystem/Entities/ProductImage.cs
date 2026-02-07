using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEcommerchSystem.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
     
        public int ProductId { get; set; }

        [ForeignKey("ProductId")] 
        public Product Product { get; set; }
        [Required]
        [MaxLength(250)]
        public string ImageUrl { get; set; } = string.Empty;    
        public bool IsImage { get; set; }   = false;   
    }
}
