using FilmStudion.DTO_s.FilmStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.User
{
    public class UserDto : FilmStudioDto, IUser
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
