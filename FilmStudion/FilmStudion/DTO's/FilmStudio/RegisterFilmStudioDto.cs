﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.DTO_s.FilmStudio
{
    public class RegisterFilmStudioDto : IRegisterFilmStudio
    {
        public string City { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
