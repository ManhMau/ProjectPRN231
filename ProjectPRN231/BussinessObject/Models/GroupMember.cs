using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class GroupMember
    {
        public int Id { get; set; }
        public string NameGroup { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? Note { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

    }

}
