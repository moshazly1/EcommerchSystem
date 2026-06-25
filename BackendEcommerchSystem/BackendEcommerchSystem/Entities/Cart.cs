using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEcommerchSystem.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }     
        [Required]  
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User?User { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now; 
        public DateTime UpdateAt {  get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 

    }
}
