using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Models.Film
{
    interface IFilm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool RentedOut { get; set; }
    }
}
