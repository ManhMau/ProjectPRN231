using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS
{
    public class UserDTO
    {
      
        public string Id { get; set; } = null!; // Từ IdentityUser
        public string UserName { get; set; } = null!;
  
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }

        // Có thể thêm các thuộc tính khác nếu cần (ví dụ: vai trò, điện thoại...)
        public string PhoneNumber { get; set; }
    }
}
