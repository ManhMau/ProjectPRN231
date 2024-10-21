using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            DocumentUsers = new HashSet<DocumentUser>();
            GroupMembers = new HashSet<GroupMember>();
        }

        public bool IsActive { get; set; }

        public virtual ICollection<DocumentUser> DocumentUsers { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }

}
