using BackendEcommerchSystem.Enums;

namespace BackendEcommerchSystem.DTOs.AcountDTO
{
    public class AuthResponseDTO
    {
        public string   Mesage { get; set; }
        public bool IsAuthentication { get ; set; }     

        public string Username { get; set; } 
        public string Email {  get; set; }       

        public string Token { get; set; }           

        public DateTime ExpiresOn { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public DateTime? TwoFactorCodeExpiresAt { get; set; }
    }
}
