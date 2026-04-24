namespace BackendEcommerchSystem.DTOs.AcountDTO
{
    public class ResetPaswordDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
