using WebApp4_Jwt.DataStore;

namespace WebApp4_Jwt.Abstraction
{
    public interface IUserService
    {
        public Guid UserAdd(string name, string password, UserRole roleId);
        public string CheckUserRole(string name, string password);
    }
}
