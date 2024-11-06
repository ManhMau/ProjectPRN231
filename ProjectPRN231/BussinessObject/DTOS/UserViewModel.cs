using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        [Required]

        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? GroupId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IEnumerable<GroupMemberDTO> GroupMembers { get; set; } = new List<GroupMemberDTO>();
    }
}
