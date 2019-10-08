using AutoMapper;
using SC.API.Models;
using SC.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<League, LeagueVM>();
            CreateMap<LeagueVM, League>();
        }
    }
}
