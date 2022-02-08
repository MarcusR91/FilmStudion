using FilmStudion.Data;
using FilmStudion.DTO_s.FilmStudio;
using FilmStudion.DTO_s.User;
using FilmStudion.Models.FilmStudio;
using FilmStudion.Models.User;
using FilmStudion.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {

        //private readonly IUserRepository _userRepo;
        private readonly IFilmStudioRepository _studioRepo;
        private readonly AppDbContext _db;


        public FilmStudioController(IFilmStudioRepository studioRepo, AppDbContext db)
        {
            _studioRepo = studioRepo;
            _db = db;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterFilmStudioDto model)
        {
            bool studioIsUnique = _studioRepo.StudioIsUnique(model.Name);

            if (!studioIsUnique)
            {
                return BadRequest(new { message = "This username already exists" });
            }

            var studio = _studioRepo.Register(model.Name, model.Password, model.City);

            return Ok(new
            {
                Id = studio.StudioId,
                Username = studio.Name,
                City = studio.City,
                Role = studio.Role,

            });
        }


        [HttpGet("Getstudios")]
        public IActionResult GetAllStudios()
        {

            var user = this.User.IsInRole("filmstudio");

            if (user)
            {
                var studios = from b in _db.FilmStudios
                              select new GetFilmStudioDto()
                              {
                                  StudioId = b.StudioId,
                                  Name = b.Name,
                                  Role = b.Role

                              };
                return Ok(studios);
            }
            if (this.User.IsInRole("admin"))
            {
                var studios = _studioRepo.GetAll();
                return Ok(studios);
            }

            var unauthorizedUser = from b in _db.FilmStudios
                                   select new GetFilmStudioDto()
                                   {
                                       StudioId = b.StudioId,
                                       Name = b.Name,
                                       Role = b.Role

                                   };

            return BadRequest(unauthorizedUser);

        }
    }
}
