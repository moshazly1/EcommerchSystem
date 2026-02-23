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

        public async Task DeleteUserAsync(int id)
        {
            var user = await _appDbContext.users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _appDbContext.users.Remove(user);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
           var  user =   await _appDbContext.users.ToListAsync();
         return user;   
        }

        public async Task<User> GetByEmailAsync(string email)
        {
         return   await _appDbContext.users.FirstOrDefaultAsync(u=>u.Email == email); 
        }

        public Task<User> GetByIDAsync(int id)
        {
            return _appDbContext.users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
         await  _appDbContext.SaveChangesAsync();
        }

        public void Update(User user)
        {
           _appDbContext.users.Update(user);
        }
    }
}
