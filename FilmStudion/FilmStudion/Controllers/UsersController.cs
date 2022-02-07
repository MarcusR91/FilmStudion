using FilmStudion.DTO_s.FilmStudio;
using FilmStudion.DTO_s.User;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IFilmStudioRepository _studioRepo;


        public UsersController(IUserRepository userRepo, IFilmStudioRepository studioRepo)
        {
            _userRepo = userRepo;
            _studioRepo = studioRepo;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDto model)
        {
            if (model.UserName !=null) //Check if username for admin is not null
            {
                var user = _userRepo.Authenticate(model.UserName, model.UserPassword);
                if (user == null)
                {
                    return BadRequest("The username or password is incorrect.");
                }

                return Ok(new
                {
                    Id = user.UserId,
                    Username = user.UserName,
                    Role = user.Role,
                    Token = user.Token
                });
            }

            else
            {
                var studio = _studioRepo.Authenticate(model.StudioName, model.StudioPassword);
                if (studio == null) 
                {
                    return BadRequest("The username or password is incorrect.");
                }

                return Ok(new
                {
                    Id = studio.StudioId,
                    Username = studio.Name,
                    City = studio.City,
                    Role = studio.Role,
                    Token = studio.Token
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO model)
        {
            bool userIsUnique = _studioRepo.StudioIsUnique(model.UserName);

            if (!userIsUnique)
            {
                return BadRequest(new { message = "This username already exists" });
            }

            var user = _userRepo.Register(model.UserName, model.Password);

            return Ok(new
            {
                id = user.UserId,
                username = user.UserName,
                role = user.Role
            });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Getusers")]
        public IActionResult GetAll()
        {
            var users = _userRepo.GetAllUsers();
            return Ok(users);
        }

    }
}
