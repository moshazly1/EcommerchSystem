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

    }
}
