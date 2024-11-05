namespace BussinessObject.DTOS
{
    public class GroupMemberDTO
    {
        public int Id { get; set; }
        public string NameGroup { get; set; }
        public bool IsActive { get; set; }
        public string? Note { get; set; }


        public List<UserDTO> Users { get; set; } = new List<UserDTO>();


    }
}
