using FilmStudion.Models.FilmStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Repositories.Interface
{
    public interface IFilmStudioRepository
    {
        bool StudioIsUnique(string userName);
        FilmStudio Authenticate(string userName, string userPassword);
        FilmStudio Register(string userName, string UserPassword, string city);
        public IEnumerable<FilmStudio> GetAll();
        public FilmStudio GetFilmStudioById(int filmStudioId);
    }
}
