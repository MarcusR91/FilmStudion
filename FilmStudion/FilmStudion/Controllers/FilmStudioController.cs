using FilmStudion.DTO_s.FilmStudio;
using FilmStudion.Models.FilmStudio;
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


        public FilmStudioController(IFilmStudioRepository studioRepo)
        {
            _studioRepo = studioRepo;
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
        public IActionResult GetAll()
        {
            var studios = _studioRepo.GetAll();
            return Ok(studios);
        }
    }
}
