using AutoMapper;
using FilmStudion.DTO_s.FilmStudio;
using FilmStudion.DTO_s.User;
using FilmStudion.Models.FilmStudio;
using FilmStudion.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<FilmStudio, FilmStudioDto>().ReverseMap();
        }
    }
}
