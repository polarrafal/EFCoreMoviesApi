using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedApi.Dto;
using WebApi.DAL;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.CinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            movieDto.Cinemas = movieDto.Cinemas.DistinctBy(x => x.Id).ToList();
            return Ok(movieDto);
        }
    }
}
