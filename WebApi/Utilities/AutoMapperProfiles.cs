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
        }
    }
}
