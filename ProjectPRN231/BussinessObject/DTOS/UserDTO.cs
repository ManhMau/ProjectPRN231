using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS
{
    public class UserDTO
    {

        public string Id { get; set; } = null!; // From IdentityUser
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
