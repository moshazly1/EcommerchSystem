using BackendEcommerchSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.Entities
{
    public class User
    {
        [Key]
    public  int Id { get; set; }

        [MaxLength(150)]
        [Required]
     public   string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; 
        [Required]
       public  UserRole Role { get; set; } = UserRole.Customer;  
      public   bool IsAcive { get; set; } = true;         

       public   DateTime CreatedAt { get; set; }  = DateTime.Now;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }


        // Forgot Password
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public   ICollection<Order> Orders { get; set; }  = new List<Order>();
        public ICollection<WhiteList> WishList { get; set; } = new List<WhiteList>(); 
    
    }
}
