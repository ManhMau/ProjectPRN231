using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using DataAccess.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Services;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository ;
        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost("Login")]
        public async Task<APIResponseModel> Login([FromBody] LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage).ToList();
                    return new APIResponseModel()
                    {
                        Code = 400,
                        Data = errors,
                        IsSuccess = false,
                        Message = string.Join(";", errors)
                    };

                }
                var result = await userRepository.Login(model);
                return result;

            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }
        [HttpPost("Register")]
        public async Task<APIResponseModel> Register([FromBody] RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage).ToList();
                    return new APIResponseModel()
                    {
                        Code = 400,
                        Data = errors,
                        IsSuccess = false,
                        Message = string.Join(";", errors)
                    };

                }
                var result = await userRepository.Register(model);
                return result;

            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }
        [HttpGet("GetAllUser")]
        public async Task<List<UserDTO>> GetListUser()
        {
            try
            {
                return await userRepository.GetUsersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<UserDTO> GetDocumentById(string id)
        {
            try
            {
                return await userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetUserByName/{name}")]
        public async Task<UserDTO> GetDocumentByName(string name)
        {
            try
            {
                return await userRepository.GetUserByNameAsync(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await userRepository.UpdateUserAsync(user) ;
                return Ok(new { message = "Documents uppdated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            try
            {
                var documents = await userRepository.GetUserById(id) ;
                if (documents == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                await userRepository.DeleteUserAsync(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet("SearchUser")]
        public async Task<List<UserDTO>> SearchByTitle(string title)
        {
            try
            {
                return await userRepository.SearchUsersAsync(title);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("SortByName")]
        public async Task<List<UserDTO>> SortbyDate(bool descending)
        {
            try
            {
                return await userRepository.SortByName(descending);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
