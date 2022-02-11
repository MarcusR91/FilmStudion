using FilmStudion.DTO_s.Film;
using FilmStudion.Models.Film;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Repositories.Interface
{
    public interface IFilmRepository
    {
        public bool CreateFilm(Film film);
        bool Save();
        public IEnumerable<Film> GetFilms();
        public Film GetFilmById(int id);
        public Film PatchFilm(int id, JsonPatchDocument Film);
        public bool UpdateFilm(Film film);
    }
}
