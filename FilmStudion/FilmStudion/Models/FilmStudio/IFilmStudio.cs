using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Models.FilmStudio
{
    interface IFilmStudio
    {
        public int StudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
    }
}
