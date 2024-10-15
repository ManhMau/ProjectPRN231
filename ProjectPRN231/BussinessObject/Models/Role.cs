using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int IdRole { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
