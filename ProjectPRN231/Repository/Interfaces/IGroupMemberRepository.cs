using BussinessObject.DTOS;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IGroupMemberRepository
    {
        Task<List<GroupMemberDTO>> GetAllGroupMembersAsync();
        Task<GroupMemberDTO> GetGroupMemberByIdAsync(int id);
        Task AddGroupMemberAsync(GroupMemberDTO groupMember);
        Task UpdateGroupMemberAsync(GroupMemberDTO groupMember);
        Task DeleteGroupMemberAsync(int id);

        // Phương thức tìm kiếm theo NameGroup
        Task<List<GroupMemberDTO>> SearchByNameGroup(string nameGroup);

        // Phương thức sắp xếp theo sortBy và sortDirection
        Task<List<GroupMemberDTO>> SortBy(string sortBy = "id", string sortDirection = "asc");
    }

}
