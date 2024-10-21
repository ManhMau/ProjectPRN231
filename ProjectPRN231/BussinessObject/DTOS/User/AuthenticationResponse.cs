using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS.User
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public long ExpriedTime { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
