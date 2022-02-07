using FilmStudion.Data;
using FilmStudion.Models.FilmStudio;
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
    public class FilmStudioRepository : IFilmStudioRepository
    {
        private readonly AppDbContext _db;
        private readonly AppSettings _appSettings;

        public FilmStudioRepository(AppDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public FilmStudio Authenticate(string userName, string userPassword)
        {
            var filmStudio = _db.FilmStudios.SingleOrDefault(x => x.Name == userName && x.Password == userPassword);

            if (filmStudio == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, filmStudio.StudioId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            filmStudio.Token = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return filmStudio;

        }

        public FilmStudio Register(string userName, string password, string city)
        {
            FilmStudio newStudio = new FilmStudio()
            {
                Name = userName,
                Password = password,
                City = city,
                Role = "Filmstudio"
            };
            _db.FilmStudios.Add(newStudio);
            _db.SaveChanges();
            newStudio.Password = "";
            return newStudio;
        }

        public bool StudioIsUnique(string userName)
        {
            var studio = _db.FilmStudios.SingleOrDefault(x => x.Name == userName);
            if (studio == null) 
                return true;

            return false;
        }
        public IEnumerable<FilmStudio> GetAll() 
        {
            return _db.FilmStudios;
        }
    }
}

