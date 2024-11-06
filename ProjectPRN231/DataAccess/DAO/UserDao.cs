using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDao
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserDao(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userManager.Users.Include(x=>x.GroupMember).ToListAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }
        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.Users.Include(x => x.GroupMember).FirstOrDefaultAsync(u => u.Id == id); 
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<IdentityResult> UpdateUserAsync(UserDTOUpdate updateUser)
        {
            // Retrieve the user to update
            var user = await _userManager.Users.Include(x => x.GroupMember).FirstOrDefaultAsync(u => u.Id == updateUser.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update properties
            user.UserName = updateUser.UserName;
            user.Email = updateUser.Email;
            user.IsActive = updateUser.IsActive;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.GroupId = updateUser.GroupId;

            // Perform the update
            var result = await _userManager.UpdateAsync(user);

            // Check for errors in IdentityResult
            if (!result.Succeeded)
            {
                // If errors are found, throw an exception or handle it accordingly
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User update failed: {errors}");
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.Users.Include(x => x.GroupMember).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _userManager.DeleteAsync(user);
        }
        public async Task<UserDTO> GetUserByName(string name)
        {
            var user = await _userManager.Users.Include(x => x.GroupMember).FirstOrDefaultAsync(u => u.UserName == name);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<List<UserDTO>> SearchUsersAsync(string searchTerm)
        {
            var users = await _userManager.Users.Include(x => x.GroupMember)
                .Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm) || u.PhoneNumber.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<List<UserDTO>>(users);
        }
        public async Task<List<UserDTO>> GetUsersSortedByNameAsync(bool descending = true)
        {
            try
            {
                var users = descending
                    ? await _userManager.Users.Include(x => x.GroupMember).OrderByDescending(u => u.UserName).ToListAsync()
                    : await _userManager.Users.Include(x => x.GroupMember).OrderBy(u => u.UserName).ToListAsync();

                return _mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}


