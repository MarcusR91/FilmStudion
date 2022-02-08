using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Models.Film
{
    public class FilmCopy : IFilmCopy
    {
        public int FilmCopyId { get; set; }
        public int FilmId { get; set; }
        public bool RentedOut { get; set; }
        public int FilmStudioId { get; set; }
        public int numberOfCopies {get;set;}
      
        
    }
}
