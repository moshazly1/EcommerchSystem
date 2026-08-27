using Azure;
using BackendEcommerchSystem.DTOs.AddNoteficationDTO;
using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;

namespace BackendEcommerchSystem.Controllers
{

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
        [Authorize(Roles = "Admin")]
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


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserById()
        {
            BaseResponseModel response = new BaseResponseModel();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            try
            {
                var user = await _userServises.GetByIDAsync(userId);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                await _userServises.DeleteUser(id);

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
        [HttpPut("UpdateUser")]
        [Authorize] 
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO update  )
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); 
        await _userServises.UpdateUser(userId, update);
            var baseResponseModel = new BaseResponseModel
            {
                Status = true,
                StatusMessage = "User updated successfully.",
                Data = null
            };
            return Ok(baseResponseModel); 
        }
        [HttpPut("email-digest")]
        [Authorize]
        public async Task<IActionResult> UpdateEmailDigest(UpdateEmailDigestDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _userServises.UpdateEmailDigestAsync(userId , dto.EmailDigest);
            return Ok("User Update Notification"); 
        }

        [HttpPut("Account-Activity")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountActivity(UpdateAccountActivityDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _userServises.UpdateAccountActivity(userId, dto.AccountActivity);
            return Ok("User  Account Activity");
        }

        [HttpPut("two-Factor")]
        [Authorize] 
        public async Task<IActionResult> UpdateTowFactor([FromBody] Update2FactorAuthanticationDTO dto )
        {
            BaseResponseModel respons = new BaseResponseModel();
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) ;

            await _userServises.EnableTwoFactorAuthantication(userid , dto.IsTwoFactorEnabled);
            respons.StatusMessage = dto.IsTwoFactorEnabled ? "Two-factor authentication enabled successfully." : "Two-factor authentication disabled successfully.";
            respons.Status = true;
            return Ok(respons);
        }
    }
}
