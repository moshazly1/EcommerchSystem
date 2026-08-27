using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class UserReposutory : IUserRepository

    {
        private readonly AppDbContext _appDbContext; 
        public UserReposutory(AppDbContext appDbContext) {
        _appDbContext = appDbContext;       
        } 
        public async Task AddAsync(User user)
        {
            await _appDbContext.users.AddAsync(user); 
        }

       
        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
           var  user =   await _appDbContext.users.ToListAsync();
           return user;   
        }

        public async Task<User> GetByEmailAsync(string email)
        {
         return   await _appDbContext.users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower().Trim()); 
        }

        public Task<User> GetByIDAsync(int id)
        {
            return _appDbContext.users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
         await  _appDbContext.SaveChangesAsync();
        }
     
        public async Task<User> GetByRefreshTokenAsync(string token)
        {

            return await _appDbContext.users.FirstOrDefaultAsync(u => u.RefreshToken == token);
        }

        public void UpdateUser(User user)
        {
            _appDbContext.users.Update(user);
        }

        public void DeleteUser(User user)
        {
            _appDbContext.users.Remove(user);
        }

        public async Task UpdateEmailDigestAsync(int userId, bool emailDigest)
        {
            var user = await _appDbContext.users.FirstOrDefaultAsync(u=>u.Id == userId); 
          if ( user == null)
            {
                throw new Exception("user Not  found"); 
            }
          user.EmailDigest = emailDigest;       
            await _appDbContext.SaveChangesAsync(); 
        }

        public async Task UpdatAaccountActivity(int userId, bool accountActivity)
        {
            var user = await _appDbContext.users.FirstOrDefaultAsync(u=>u.Id == userId);
            if (user == null )
            {
                throw new Exception("user Not Found "); 
            }
            user.AccountActivity = accountActivity;     
            await _appDbContext.SaveChangesAsync();     
        }
    }
}
