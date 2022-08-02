using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DissonantSerenity.Data;
using DissonantSerenity.Model;
namespace DissonantSerenity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<LoadController> _logger;

        // Constructor
        public LoadController(IRepository repo, ILogger<LoadController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Methods

        // GET /api/pawns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pawn>>> LoadPawns(string? key = null)
        {
            World.Main();
            IEnumerable<Pawn> pawns;
            try
            {
                pawns = await _repo.LoadPawnsAsync(key);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return pawns.ToList();
        }
    }
}
