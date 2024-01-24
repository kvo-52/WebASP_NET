using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp4_Jwt.Abstraction;
using WebApp4_Jwt.DataStore;

namespace WebApp4_Jwt.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public Guid UserAdd(string name, string password, UserRole roleId)
        {
            var users = new List<UserEntity>();
            using (_context)
            {
                var userExist = _context.Users.Where(x => !x.Login.ToLower().Equals(name.ToLower()));
                UserEntity entity = null;
                if (userExist != null)
                {
                    return default;
                }
                entity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Login = name,
                    Password = password,
                    RoleType = roleId
                };
                return entity.Id;
            }
        }

        public string CheckUserRole(string name, string password)
        {
            using (_context)
            {
                var entity = _context.Users
                    .FirstOrDefault(
                    x => x.Login.ToLower().Equals(name.ToLower()) &&
                    x.Password.Equals(password));

                if (entity == null)
                    return "";

                var user = new UserModel
                {
                    UserName = entity.Login,
                    Password = entity.Password,
                    Role = entity.RoleType
                };

                return GenerateToken(user);
            }
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
