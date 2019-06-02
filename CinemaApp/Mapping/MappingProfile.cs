using AutoMapper;
using CinemaApp.Core.Models;
using CinemaApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Actor, ActorResource>().ReverseMap();
            CreateMap<MovieCategory, MovieCategoryResource>().ReverseMap();
            CreateMap<Movie, MovieResource>().ReverseMap();
            CreateMap<CinemaOwner, CinemaOwnerResource>().ReverseMap();
            CreateMap<CinemaApp.Core.Models.Cinema, CinemaResource>().ReverseMap();
            CreateMap<CinemaScreen, CinemaScreenResource>().ReverseMap();
            CreateMap<ShowTime, ShowTimeResource>().ReverseMap();
            CreateMap<AppUser, UserResource>().ReverseMap();
        }
    }
}
