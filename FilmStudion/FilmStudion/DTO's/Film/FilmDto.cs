using FilmStudion.Models.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.Film
{
    public class FilmDto
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }
    }
}
