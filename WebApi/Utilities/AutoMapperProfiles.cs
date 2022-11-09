using AutoMapper;
using SharedApi.Dto;
using WebApi.Models.Entities;

namespace WebApi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDto>();

            CreateMap<Cinema, CinemaDto>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(p => p.Location.Y))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(p => p.Location.X));

            CreateMap<Genre, GenreDto>();

            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Genres, ent => ent.MapFrom(p => p.Genres))
                .ForMember(dto => dto.Cinemas, ent => ent.MapFrom(p => p.CinemaHalls.Select(c => c.Cinema)))
                .ForMember(dto => dto.Actors, ent => ent.MapFrom(p => p.MovieActors.Select(ma => ma.Actor)));
        }
    }
}
