using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using SharedApi.Dto;
using WebApi.Constants;
using WebApi.DAL;
using WebApi.Models.Entities;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CinemasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaDto>>> Get(int page, int recordsToTake)
        {
            var result = await _context.Cinemas
                .AsNoTracking()
                .ProjectTo<CinemaDto>(_mapper.ConfigurationProvider)
                .Paginate(page, recordsToTake)
                .ToListAsync();

            return result;
        }

        [HttpGet("closeToMe")]
        public async Task<ActionResult> GetCloseToMe(double latitude, double longitude)
        {
            const int maxDistanceInMeters = 2000; // 2km

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: CoordinatesConstants.EarthSrid);
            var myLocation = geometryFactory.CreatePoint(new Coordinate(latitude, longitude));
            var cinemas = await _context.Cinemas
                .OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation, maxDistanceInMeters))
                .Select(c => new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                })
                .ToListAsync();

            return Ok(cinemas);
        }
    }
}
