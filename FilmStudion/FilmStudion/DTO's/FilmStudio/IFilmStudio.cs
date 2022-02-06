using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.FilmStudio
{
    public interface IFilmStudio
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
