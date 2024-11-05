using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            DocumentUsers = new HashSet<DocumentUser>();
        }

        public bool IsActive { get; set; }
        public int? GroupId { get; set; }

        public virtual GroupMember? GroupMember { get; set; }


        public virtual ICollection<DocumentUser> DocumentUsers { get; set; }
    }

}
