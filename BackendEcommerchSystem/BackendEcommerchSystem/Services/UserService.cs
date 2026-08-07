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

        public  UserService (IUserRepository userRepository) {
            _userRepository = userRepository;
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
            }; 
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

        }
    }
}
