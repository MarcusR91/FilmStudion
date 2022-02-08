using AutoMapper;
using FilmStudion.DTO_s.Film;
using FilmStudion.Models.Film;
using FilmStudion.Models.User;
using FilmStudion.Repositories.Interface;
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
    public class FilmController : ControllerBase
    {

        private readonly IFilmRepository _filmRepo;
        private readonly IMapper _mapper;


        public FilmController(IFilmRepository filmRepo, IMapper mapper)
        {
            _filmRepo = filmRepo;
            _mapper = mapper;
        }

        [HttpPut("create")]
        public IActionResult CreateFilm([FromBody] CreateFilmDto model) 
        {
            if (model == null) 
            {
                return BadRequest(ModelState);
            }

            var newFilm = _mapper.Map<Film>(model);

            if (!_filmRepo.CreateFilm(newFilm))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {newFilm.Name}");
                return StatusCode(500, ModelState);
            }

            return Created("",model);

        }

        //[HttpGet]
        //public IActionResult GetFilms()
        //{

        //}
    }
}
