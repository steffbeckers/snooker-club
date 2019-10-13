using AutoMapper;
using SC.API.Models;
using SC.API.ViewModels;

namespace SC.API.DAL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();

            CreateMap<Player, PlayerVM>();
        }
    }
}
