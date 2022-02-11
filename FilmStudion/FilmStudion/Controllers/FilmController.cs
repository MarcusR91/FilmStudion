using AutoMapper;
using FilmStudion.Data;
using FilmStudion.DTO_s.Film;
using FilmStudion.Models.Film;
using FilmStudion.Models.User;
using FilmStudion.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFilmStudioRepository _studioRepo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;


        public FilmController(IFilmRepository filmRepo, IMapper mapper, AppDbContext db, IFilmStudioRepository studioRepo)
        {
            _filmRepo = filmRepo;
            _mapper = mapper;
            _db = db;
            _studioRepo = studioRepo;
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
                ModelState.AddModelError("", $"Something went wrong when saving {newFilm.Name}");
                return StatusCode(500, ModelState);
            }

            return Created("", model);

        }

        [HttpGet("GetAllFilms")]
        public IActionResult GetFilms()
        {
            var studioUser = this.User.IsInRole("filmstudio");
            var adminUser = this.User.IsInRole("admin");
            if (studioUser)
            {
                var films = from f in _db.Films
                            select new FilmDto()
                            {
                                FilmId = f.FilmId,
                                Name = f.Name,
                                ReleaseDate = f.ReleaseDate,
                                Country = f.Country,
                                Director = f.Director,
                                FilmCopies = f.FilmCopies
                            };
                return Ok(films);
            }
            else if (adminUser)
            {
                var adminFilms = from f in _db.Films
                                 select new FilmDto()
                                 {
                                     FilmId = f.FilmId,
                                     Name = f.Name,
                                     ReleaseDate = f.ReleaseDate,
                                     Country = f.Country,
                                     Director = f.Director,
                                     FilmCopies = f.FilmCopies
                                 };
                return Ok(adminFilms);
            }

            var notAuhtorized = from f in _db.Films
                                select new FilmDto()
                                {
                                    FilmId = f.FilmId,
                                    Name = f.Name,
                                    ReleaseDate = f.ReleaseDate,
                                    Country = f.Country,
                                    Director = f.Director,
                                };
            return Ok(notAuhtorized);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetFilm(int id)
        {
            if (this.User.IsInRole("filmstudio") || this.User.IsInRole("admin"))
            {
                var film = _filmRepo.GetFilmById(id);

                return Ok(film);
            }

            var newFilm = await _db.Films.Select(b =>
        new FilmDto()
        {
            FilmId = b.FilmId,
            Name = b.Name,
            ReleaseDate = b.ReleaseDate,
            Country = b.Country,
            Director = b.Director,
        }).SingleOrDefaultAsync(b => b.FilmId == id);

            return Ok(newFilm);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{filmId:int}")]
        public IActionResult PatchFilm([FromBody] JsonPatchDocument model, [FromRoute] int filmId)
        {


            var updatedFilm = _filmRepo.PatchFilm(filmId, model);


            return Ok(updatedFilm);

        }

        [Authorize(Roles = "admin")]
        [HttpPut("{filmId:int}")]
        public IActionResult UpdateFilm(int filmId, [FromBody] FilmDto model) 
        {
            //var updateFilm = _filmRepo.UpdateFilm(model.FilmId);

            //return Ok(updateFilm);

            if (model == null)
            {
                return BadRequest(ModelState); //If an error is encountered Modelstate will show it
            }

            var updatefilm = _mapper.Map<Film>(model);

            if (!_filmRepo.UpdateFilm(updatefilm))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {updatefilm.FilmId}");
                return StatusCode(500, ModelState);
            }
            return Ok(updatefilm);
        }

        //[HttpPost("{id:int}")]
        //public IActionResult Rent(int filmId, int studioId)
        //{
        //    var rentFilm = _filmRepo.GetFilmById(filmId);
        //    var studio = _studioRepo.GetFilmStudioById(studioId);

        //    var filmCopy = new FilmCopyDto();

        //    rentFilm.FilmId = studio.StudioId;
        //    filmCopy.numberOfCopies += 1;
            

        //    return Ok(filmCopy);
        //}
    }
}
