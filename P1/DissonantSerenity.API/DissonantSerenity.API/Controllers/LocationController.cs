using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DissonantSerenity.Data;
using DissonantSerenity.Model;
namespace DissonantSerenity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ITempData _repo;
        private readonly ILogger<LocationController> _logger;

        // Constructor
        public LocationController(ITempData repo, ILogger<LocationController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Methods

        // GET /api/pawns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Token>>> LoadLocation(string loc)
        {
            IEnumerable<Token> tokens;
            try
            {
                tokens = await _repo.ObserveLocation(loc);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return tokens.ToList();
        }
    }
}
