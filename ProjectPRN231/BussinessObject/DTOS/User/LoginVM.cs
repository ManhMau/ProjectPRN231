using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
     
    }
}
