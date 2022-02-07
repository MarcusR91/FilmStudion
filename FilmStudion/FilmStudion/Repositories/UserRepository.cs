using FilmStudion.Data;
using FilmStudion.Models.User;
using FilmStudion.Repositories.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FilmStudion.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly AppSettings _appSettings;

        public UserRepository(AppDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string userName, string userPassword)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName && x.Password == userPassword);

            if (user == null) 
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
             user.Token = tokenHandler.WriteToken(token);

            
            return user;

        }

        public User GetExistingUser(int Id) 
        {
            return _db.Users.FirstOrDefault(x => x.UserId == Id);
        }
        public User Register(string userName, string password)
        {
            User newAdmin = new User()
            {
                UserName = userName,
                Password = password,
                isAdmin = true,
                Role = "admin"
            };
            _db.Users.Add(newAdmin);
            _db.SaveChanges();
            newAdmin.Password = "";
            return newAdmin;
        }

        public IEnumerable<User> GetAllUsers() 
        {
            return _db.Users;
        }

        public bool UserIsUnique(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
