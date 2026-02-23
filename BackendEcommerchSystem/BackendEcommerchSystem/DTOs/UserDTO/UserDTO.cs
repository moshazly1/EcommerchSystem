using BackendEcommerchSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
      
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } 
        public bool IsAcive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
