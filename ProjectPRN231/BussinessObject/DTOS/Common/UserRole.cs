namespace BussinessObject.DTOS.Common
{
    public class UserRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public static readonly List<string> Roles = new List<string> { Admin, User };
    }
}
