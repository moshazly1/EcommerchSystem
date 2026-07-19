using BackendEcommerchSystem.DTOs.WhiteListDTO;
using BackendEcommerchSystem.Migrations;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IWhiteListServices
    {
        Task<IEnumerable<WhiteListResponseDto>> GetWhiteListaysnc(int userId);

        Task AddAsync(  int userId ,  AddWhiteListDTO dto);
        Task RemoveAsync( int userId   ,AddWhiteListDTO dto);
     
      
    }
}
