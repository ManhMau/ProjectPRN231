using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using BussinessObject.Models;
using DataAccess.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Services
{
    public class UserServices : IUserRepository
    {
        private UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly SignInManager<User> signInManager;
        public readonly AdminAccount adminAccount;
        public readonly JwtSetting jwtSetting;
        public readonly ProjectPRN231Context context;
        private readonly UserDao userDao;
        public IMapper mapper;

        public UserServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager,
           IOptionsMonitor<AdminAccount> adminAccount, IOptionsMonitor<JwtSetting> jwtSetting, ProjectPRN231Context context, IMapper mapper, UserDao userDao)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.adminAccount = adminAccount.CurrentValue;
            this.jwtSetting = jwtSetting.CurrentValue;
            this.context = context;
            this.mapper = mapper;
            this.userDao = userDao;
        }

        public async Task<APIResponseModel> Login(LoginVM model)
        {
            try
            {
                var secretKeyBytes = Encoding.UTF8.GetBytes(jwtSetting.Key);
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // Validate login credentials
                var result = await ValidLogin(model);
                if (!result.IsSuccess)
                {
                    return result;
                }

                // Get user claims
                var claims = await GetClaimsUsers(model);

                // Create token
                var tokenDescription = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(3),
                    Issuer = jwtSetting.Issuer,
                    Audience = jwtSetting.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                string accessToken = jwtTokenHandler.WriteToken(token);

                // Return success with the generated token
                result.Data = accessToken;
                return result;
            }
            catch (Exception ex)
            {
                // Log exception details for further analysis (optional)
                // _logger.LogError(ex, "Error during user login");

                // Return a generic error message to the client
                return new APIResponseModel()
                {
                    Code = 500,
                    IsSuccess = false,
                    Message = "An error occurred during the login process. Please try again later.",
                    Data = ex.Message // You might not want to expose exception details in production
                };
            }
        }

        private async Task<List<Claim>> GetClaimsUsers(LoginVM model)
        {
            List<Claim> result;
            var user = await userManager.FindByNameAsync(model.UserName);
            var roles = await userManager.GetRolesAsync(user);
            string role = roles[0].ToString();
            result = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new("UserId", user.Id),
                new(ClaimTypes.Role, role),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return result;
        }
        private async Task<APIResponseModel> ValidLogin(LoginVM user)
        {
            var result = new APIResponseModel()
            {
                Code = 200,
                Message = "Ok",
                IsSuccess = true,
            };
            var userIdentity = await userManager.FindByNameAsync(user.UserName);
            if (userIdentity == null || !await userManager.CheckPasswordAsync(userIdentity, user.Password))
            {
                if (user.UserName == adminAccount.Username)
                {
                    var admimmAcount = await userManager.FindByNameAsync(adminAccount.Username);
                    if (admimmAcount == null)
                    {
                        var admin = new User()
                        {
                            Email = user.UserName,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = user.UserName,
                          
                        };
                        var resultCreateUser = await userManager.CreateAsync(admin, adminAccount.Password);
                        var resulyRole = await userManager.AddToRoleAsync(admin, "Admin");
                    }
                }
                else
                {
                    return new APIResponseModel
                    {
                        Code = 400,
                        IsSuccess = false,
                        Message = "Username or password is incorrect!",
                    };
                }
            }
            return result;
        }

        public async Task<APIResponseModel> Register(RegisterVM model)
        {
            APIResponseModel result = new APIResponseModel()
            {
                Code = 200,
                IsSuccess = true,
                Message = "User created success",
                Data = model
            };
            var userExistMail = await userManager.FindByEmailAsync(model.Email);
            var userExistName = await userManager.FindByNameAsync(model.Username);
            if (userExistMail != null || userExistName != null)
            {
                return new APIResponseModel()
                {
                    Code = 400,
                    Message = "User has been already existed!",
                    IsSuccess = false
                };
            }
            var user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
               
            };
            var resultCreateUser = await userManager.CreateAsync(user, model.Password);
            var resultRole = await userManager.AddToRoleAsync(user, "User");

            if (!resultCreateUser.Succeeded)
            {
                return new APIResponseModel()
                {
                    Code = 400,
                    Message = "Error when create user",
                    IsSuccess = false
                };
            }
            return result;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
           return await userDao.GetUsersAsync();
        }

        public async Task<UserDTO> GetUserById(string id)
        {
            return await userDao.GetUserByIdAsync(id);
        }

        public Task<IdentityResult> UpdateUserAsync(UserDTO updateUser)
        {
            return userDao.UpdateUserAsync(updateUser);
        }

        public Task<IdentityResult> DeleteUserAsync(string id)
        {
            return userDao.DeleteUserAsync(id);
        }

        public Task<UserDTO> GetUserByNameAsync(string name)
        {
           return userDao.GetUserByName(name);
        }

        public Task<List<UserDTO>> SearchUsersAsync(string search)
        {
            return userDao.SearchUsersAsync(search);
        }

        public Task<List<UserDTO>> SortByName(bool descending)
        {
            return userDao.GetUsersSortedByNameAsync(descending);
        }
    }

}





