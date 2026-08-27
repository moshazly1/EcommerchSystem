using BackendEcommerchSystem.DTOs.AddNoteficationDTO;
using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Enums;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using System.Data;

namespace BackendEcommerchSystem.Services
{
    public class UserService : IUserServises
    {
        private readonly IUserRepository _userRepository; 
        private readonly IEmailService _emailService;    

        public  UserService (IUserRepository userRepository , IEmailService emailService) {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task AddUserAsync(CreateUserDTO userDTO)
        {
            var user = new User
            {
            
                FullName = userDTO.FullName,
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Role = Enum.Parse<UserRole>(userDTO.Role.ToString()),
                IsAcive = true,
                CreatedAt = DateTime.Now
            };  
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();    
        }

      

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            if (user == null) {
                throw new Exception("User not found.");
            }
            _userRepository.DeleteUser(user); 
           await  _userRepository.SaveChangesAsync(); 

        }

        public async Task EnableTwoFactorAuthantication(int userId, bool isEnble)
        {
            var user = await _userRepository.GetByIDAsync(userId);
            if (user == null) {
                throw new Exception("User Not  Found"); 
            }
            user.TwoFactorEnabled = isEnble;        
            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            var user =   await _userRepository.GetAllUserAsync();
            return user.Select(x => new UserDTO {
            CreatedAt = x.CreatedAt,    
            Email = x.Email,        
            FullName = x.FullName,      
            Id = x.Id,      
            IsAcive = x.IsAcive,  
            });
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return new UserDTO
            {
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                IsAcive = user.IsAcive,

            };
        }

        public async Task<UserDTO> GetByIDAsync(int id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            if (user == null) return null;
            return new UserDTO
            {
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber , 
                IsAcive = user.IsAcive,
                Role = user.Role.ToString(),
                AccountActivity= user.AccountActivity,  
                EmailDigest= user.EmailDigest,       
                IsTwoFactorAuth = user.TwoFactorEnabled,
            }; 
        }

        public async Task UpdateAccountActivity(int userId, bool emailDigestDto)
        {
            await _userRepository.UpdatAaccountActivity(userId  , emailDigestDto); 
        }

        public async Task UpdateEmailDigestAsync(int userId, bool emailDigest)
        {
            await _userRepository.UpdateEmailDigestAsync(userId, emailDigest); 
        }

        public async  Task   UpdateUser(int id, UpdateUserDTO update)
        {
            var user   =  await  _userRepository.GetByIDAsync(id);
          if  (user  == null )
          {
           throw new Exception("User Not  Found ");
          }
            user.FullName = update.Fullname; 
            user.PhoneNumber =  update.PhoneNumber;
            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
            string body = $@"
<div style='font-family:Arial,sans-serif;max-width:600px;margin:auto;border:1px solid #e5e7eb;border-radius:8px;padding:30px'>

<h2 style='color:#2563eb;'>👤 Profile Updated</h2>

<p>Hello <strong>{user.FullName}</strong>,</p>

<p>Your account profile has been updated successfully.</p>

<div style='background:#f8fafc;padding:15px;border-left:4px solid #2563eb;margin:20px 0'>
    <strong>Updated At:</strong> {DateTime.Now:dddd, dd MMMM yyyy HH:mm}
</div>

<p>If you made these changes, no further action is required.</p>

<p style='color:#dc2626'>
<strong>If you did not update your profile, please secure your account immediately and contact our support team.</strong>
</p>

<hr>

<p style='font-size:13px;color:gray'>
This is an automated security notification from TekStore.
</p>

</div>";
            if (user.AccountActivity)
            {
                await _emailService.SendEmailAsync(
                    user.Email,
                    "👤 Profile Updated Successfully",
                    body
                );
            }

        }
    }
}
