using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class GroupMemberDAO
    {
        private readonly ProjectPRN231Context _context;

        public GroupMemberDAO(ProjectPRN231Context context)
        {
            _context = context;
        }

        // Lấy tất cả các GroupMembers và bao gồm thông tin User
        public async Task<List<GroupMember>> GetAllGroupMembersAsync()
        {
            return await _context.GroupMembers
                .Include(gm => gm.User)  // Bao gồm thông tin User để lấy Username
                .ToListAsync();
        }
     

        // Lấy GroupMember theo Id
        public async Task<GroupMember> GetGroupMemberByIdAsync(int id)
        {
            return await _context.GroupMembers
                .Include(gm => gm.User)  // Bao gồm thông tin User
                .FirstOrDefaultAsync(gm => gm.Id == id);
        }

        // Thêm mới GroupMember
        public async Task AddGroupMemberAsync(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();
        }

        // Cập nhật GroupMember
        public async Task UpdateGroupMemberAsync(GroupMember groupMember)
        {
            _context.GroupMembers.Update(groupMember);
            await _context.SaveChangesAsync();
        }

        // Xóa GroupMember
        public async Task DeleteGroupMemberAsync(int id)
        {
            var groupMember = await _context.GroupMembers.FindAsync(id);
            if (groupMember != null)
            {
                _context.GroupMembers.Remove(groupMember);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<GroupMember>> SearchGroupMembersByNameAsync(string nameGroup)
        {
            if (string.IsNullOrWhiteSpace(nameGroup))
                throw new ArgumentNullException(nameof(nameGroup));

            try
            {
                var groupMembers = await _context.GroupMembers
                    .Where(gm => gm.NameGroup.Contains(nameGroup))
                    .ToListAsync();

                return groupMembers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching group members by name: " + ex.Message);
            }
        }
        public async Task<List<GroupMember>> SortGroupMembersAsync(string sortBy = "id", string sortDirection = "asc")
        {
            try
            {
                var query = _context.GroupMembers.AsQueryable();

                // Xử lý sắp xếp theo cột
                switch (sortBy.ToLower())
                {
                    case "id":
                        query = sortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(gm => gm.Id)
                            : query.OrderBy(gm => gm.Id);
                        break;

                    case "namegroup":
                        query = sortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(gm => gm.NameGroup)
                            : query.OrderBy(gm => gm.NameGroup);
                        break;
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error sorting group members: " + ex.Message);
            }
        }


    }
}
