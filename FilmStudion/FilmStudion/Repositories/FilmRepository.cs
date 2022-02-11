using FilmStudion.Data;
using FilmStudion.DTO_s.Film;
using FilmStudion.Models.Film;
using FilmStudion.Repositories.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDbContext _db;
        //private readonly AppSettings _appSettings;

        public FilmRepository(AppDbContext db)
        {
            _db = db;
            
        }

        public bool CreateFilm(Film newFilm) 
        {
            _db.Films.Add(newFilm);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public IEnumerable<Film> GetFilms()
        {
             return _db.Films.OrderBy(x=>x.Name).Include(x => x.FilmCopies).ToList();
        }

        public Film GetFilmById(int id) 
        {
            return _db.Films.Include(c => c.FilmCopies).FirstOrDefault(a => a.FilmId == id);
        }
        public Film PatchFilm(int filmId ,JsonPatchDocument Film)
        {
            var film = _db.Films.Find(filmId);

            if (film != null) 
            {
                Film.ApplyTo(film);
                _db.SaveChanges();
            }
            return film;
            //_db.Films.Update(Film);
            //return Save();
        }
        public bool UpdateFilm(Film film) 
        {
            _db.Films.Update(film);
            return Save();
        }
       
    }
}
