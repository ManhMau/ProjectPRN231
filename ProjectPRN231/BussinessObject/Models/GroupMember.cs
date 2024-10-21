using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class GroupMember
    {
        public int Id { get; set; }
        public string NameGroup { get; set; } = null!;
        public string? UserId { get; set; }

        public virtual User? User { get; set; }
    }

}
