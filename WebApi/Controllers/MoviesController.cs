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

        [HttpGet("groupedByCinema")]
        public async Task<ActionResult> GetGroupedByIsInCinema()
        {
            var groupedMovies = await _context.Movies
                .GroupBy(m => m.InCinemas)
                .Select(g => new
                {
                    InCinemas = g.Key,
                    Count = g.Count(),
                    Movies = g.ToList()
                }).ToListAsync();

            return Ok(groupedMovies);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Filter([FromQuery] MovieFilterDto movieFilterDto)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(movieFilterDto.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDto.Title));
            }

            if (movieFilterDto.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InCinemas);
            }

            if (movieFilterDto.UpcomingRelease)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
            }

            if (movieFilterDto.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDto.GenreId));
            }

            var movies = await moviesQueryable.Include(m => m.Genres).ToListAsync();
            var moviesDto = _mapper.Map<List<MovieDto>>(movies);

            return Ok(moviesDto);
        }
    }
}
