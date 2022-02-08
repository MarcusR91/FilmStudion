using FilmStudion.Models.Film;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Models.FilmStudio
{
    interface IFilmStudio
    {
        [Key]
        public int StudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
       

    }
}
