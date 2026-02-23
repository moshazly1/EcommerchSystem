using Azure;
using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServises _userServises;
        public UserController(IUserServises userServises)
        {
            _userServises = userServises;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var users = await _userServises.GetAllUser();
                // ملاحظة صغيرة: تأكد من فحص null الأول قبل Any() لتجنب Exception
                if (users == null || !users.Any())
                {
                    response.Status = false;
                    response.StatusMessage = "No users found in the system.";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "User retrieved successfully.";
                response.Data = users;
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "An error occurred while fetching users.";
                return StatusCode(500, response);
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var user = await _userServises.GetByIDAsync(id);
                if (user == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No user found in the system.";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "User retrieved successfully.";
                response.Data = user;
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "An error occurred while fetching user.";
                return StatusCode(500, response);
            }
        }


        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var user = await _userServises.GetByEmailAsync(email);
                if (user == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No user found with this email.";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "User retrieved successfully.";
                response.Data = user;
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "An error occurred while fetching user.";
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDTO dto)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {

                await _userServises.AddUserAsync(dto);


                response.Status = true;
                response.StatusMessage = "User created successfully by Admin.";

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {

                response.Status = false;
                response.StatusMessage = "An error occurred while creating the user: " + ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                await _userServises.DeleteUserAsync(id);

                response.Status = true;
                response.StatusMessage = "User deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = "An error occurred while deleting the user.";
                return StatusCode(500, response);
            }
        }

    }
}
