using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.User
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
