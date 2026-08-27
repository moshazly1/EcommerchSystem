using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.AcountDTO
{
    public class ResendTowFactorDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }       
    }
}
