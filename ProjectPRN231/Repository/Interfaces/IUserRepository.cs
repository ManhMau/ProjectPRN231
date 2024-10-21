using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<APIResponseModel> Login(LoginVM model);
        Task<APIResponseModel> Register(RegisterVM model);
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserById(string id);
        Task<IdentityResult> UpdateUserAsync(UserDTO updateUser);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<UserDTO> GetUserByNameAsync(string name);
        Task<List<UserDTO>> SearchUsersAsync(string search);
        Task<List<UserDTO>> SortByName(bool descending);



    }
}
