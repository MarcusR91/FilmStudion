using FilmStudion.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Repositories.Interface
{
    public interface IUserRepository
    {
        bool UserIsUnique(string userName);
        User Authenticate(string userName, string userPassword);
        User Register(string userName, string UserPassword);
    }
}
