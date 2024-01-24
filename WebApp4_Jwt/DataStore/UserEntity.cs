namespace WebApp4_Jwt.DataStore
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole RoleType { get; set; }
        public virtual Role Role { get; set; }
    }
}
