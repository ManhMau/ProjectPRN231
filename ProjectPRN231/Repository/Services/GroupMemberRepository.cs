using AutoMapper;
using BusinessObject.DTO;
using BussinessObject.Models;
using DataAccess.DAO;
using Repository.Interfaces;
using System.Collections.Generic;

namespace Repository.Services
{
    public class GroupMemberRepository : IGroupMemberRepository
    {
        private readonly GroupMemberDAO _groupMemberDAO;
        private readonly IMapper _mapper;

        public GroupMemberRepository(GroupMemberDAO groupMemberDAO, IMapper mapper)
        {
            _groupMemberDAO = groupMemberDAO;
            _mapper = mapper;
        }

        // Lấy tất cả GroupMembers và ánh xạ chúng sang DTO
        public async Task<List<GroupMemberDTO>> GetAllGroupMembersAsync()
        {
            var groupMembers = await _groupMemberDAO.GetAllGroupMembersAsync();
            return _mapper.Map<List<GroupMemberDTO>>(groupMembers);
        }

        // Lấy GroupMember theo ID và ánh xạ sang DTO
        public async Task<GroupMemberDTO> GetGroupMemberByIdAsync(int id)
        {
            var groupMember = await _groupMemberDAO.GetGroupMemberByIdAsync(id);
            return _mapper.Map<GroupMemberDTO>(groupMember);
        }

        // Thêm mới GroupMember từ DTO
        public async Task AddGroupMemberAsync(GroupMemberDTO groupMemberDTO)
        {
            var groupMember = _mapper.Map<GroupMember>(groupMemberDTO);
            await _groupMemberDAO.AddGroupMemberAsync(groupMember);
        }

        // Cập nhật GroupMember từ DTO
        public async Task UpdateGroupMemberAsync(GroupMemberDTO groupMemberDTO)
        {
            var groupMember = _mapper.Map<GroupMember>(groupMemberDTO);
            await _groupMemberDAO.UpdateGroupMemberAsync(groupMember);
        }

        // Xóa GroupMember
        public async Task DeleteGroupMemberAsync(int id)
        {
            await _groupMemberDAO.DeleteGroupMemberAsync(id);
        }
        public async Task<List<GroupMemberDTO>> SearchByNameGroup(string nameGroup)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameGroup))
                {
                    throw new ArgumentNullException(nameof(nameGroup), "NameGroup cannot be null or empty.");
                }

                // Gọi phương thức từ DAO và ánh xạ kết quả từ GroupMember sang GroupMemberDTO
                var groupMembers = await _groupMemberDAO.SearchGroupMembersByNameAsync(nameGroup);
                return _mapper.Map<List<GroupMemberDTO>>(groupMembers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while searching for group members by name: " + ex.Message);
            }
        }


        public async Task<List<GroupMemberDTO>> SortBy(string sortBy = "id", string sortDirection = "asc")
        {
            try
            {
                // Gọi phương thức từ DAO và ánh xạ kết quả từ GroupMember sang GroupMemberDTO
                var groupMembers = await _groupMemberDAO.SortGroupMembersAsync(sortBy, sortDirection);
                return _mapper.Map<List<GroupMemberDTO>>(groupMembers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while sorting group members: " + ex.Message);
            }
        }




    }
}
