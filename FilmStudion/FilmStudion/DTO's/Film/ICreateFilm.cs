using FilmStudion.Models.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.Film
{
    public interface ICreateFilm
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        //public int NumberOfCopies { get; set; }

        public List<FilmCopy> FilmCopies {get;set;}
    }
}
