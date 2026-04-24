using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.AcountDTO
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
    }
}
