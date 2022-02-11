using AutoMapper;
using FilmStudion.DTO_s.Film;
using FilmStudion.DTO_s.FilmStudio;
using FilmStudion.DTO_s.User;
using FilmStudion.Models.Film;
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
            CreateMap<FilmStudio, RegisterFilmStudioDto>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<Film, FilmDto>().ReverseMap();
            CreateMap<Film, CreateFilmDto>().ReverseMap();
            CreateMap<FilmStudio, GetFilmStudioDto>().ReverseMap();
            CreateMap<FilmStudio, GetFilmStudioLimitedDto>().ReverseMap();
            CreateMap<Film, UpdateFilmDto>().ReverseMap();
            CreateMap<FilmCopy, FilmCopyDto>().ReverseMap();
        }
    }
}
