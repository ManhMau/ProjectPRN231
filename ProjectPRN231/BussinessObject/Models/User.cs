using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class User
    {
        public User()
        {
            DocumentUsers = new HashSet<DocumentUser>();
            GroupMembers = new HashSet<GroupMember>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int IdRole { get; set; }
        public bool IsActive { get; set; }

        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<DocumentUser> DocumentUsers { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }
}
