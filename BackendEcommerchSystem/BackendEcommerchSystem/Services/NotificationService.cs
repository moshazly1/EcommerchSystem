using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class NotificationService : INotificationService
    {

        private readonly IUserRepository _userRepository;
       private readonly IEmailService _emailService;    
        private readonly IProductRepository _productRepository; 
        public NotificationService(IUserRepository userRepository , IEmailService emailService  , IProductRepository productRepository) {
       _emailService = emailService;    
            _userRepository = userRepository;   
            _productRepository = productRepository;     
        }        
        public async Task SendEmailDigestAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            var Products = await _productRepository.GetProductsAddedLastWeekAsync(); 

            foreach (var user in users) { 
             if(!Products.Any())
               {
                    return; 
                }
             if(!user.EmailDigest)
             {
                  continue;    
             }

                string body = "<h2>New Products This Week</h2>";
                body += $"<p>Hello {user.FullName},</p>";
                body += "<p>Check out the new products added this week:</p>";
                body += "<ul>";

                foreach (var product in Products)
                {
                    body += $"<li>{product.Name}</li>";
                }

                body += "</ul>";

                await _emailService.SendEmailAsync(
                    user.Email,
                    "🛍️ Weekly New Products",
                    body
                );
            }
        }
    }
}
