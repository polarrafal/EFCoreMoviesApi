using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Models.Entities;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get(int page, int recordsToTake)
        {
            var result = await _context.Genres.AsNoTracking().Paginate(page, recordsToTake).ToListAsync();
            return Ok(result);
        }
    }
}
