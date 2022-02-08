using FilmStudion.Models.FilmStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.FilmStudio
{
    public class GetFilmStudioDto
    {
        public int StudioId { get; set; }
        public string Name { get; set; }
       
        public string Role { get; set; }

    }
}
