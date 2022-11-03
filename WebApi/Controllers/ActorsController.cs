using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Models.Entities;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(int page, int recordsToTake)
        {
            var result = await _context.Actors.AsNoTracking().Paginate(page, recordsToTake).ToListAsync();
            return Ok(result);
        }
    }
}
