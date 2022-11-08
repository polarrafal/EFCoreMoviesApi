using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedApi.Dto;
using WebApi.DAL;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> Get(int page, int recordsToTake)
        {
            var result = await _context.Actors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
                .Paginate(page, recordsToTake)
                .ToListAsync();

            return Ok(result);
        }
    }
}
