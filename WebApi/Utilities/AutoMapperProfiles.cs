using AutoMapper;
using SharedApi.Dto;
using WebApi.Models.Entities;

namespace WebApi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();

            CreateMap<Cinema, CinemaDto>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(p => p.Location.Y))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(p => p.Location.X));
        }
    }
}
