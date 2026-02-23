using BackendEcommerchSystem.Enums;

namespace BackendEcommerchSystem.DTOs.UserDTO
{
    public class CreateUserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }   =string.Empty;  
        public UserRole Role { get; set; } = UserRole.Customer;
        public bool IsAcive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
