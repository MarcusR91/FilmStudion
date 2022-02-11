using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.Film
{
    public class FilmCopyDto
    {
        [Required]
        public int FilmCopyId { get; set; }
        [Required]
        public int FilmId { get; set; }
        public int numberOfCopies { get; set; }
       
    }
}
