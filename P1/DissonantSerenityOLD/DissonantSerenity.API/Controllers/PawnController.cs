using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DissonantSerenity.Data;
using DissonantSerenity.Model;

namespace DissonantSerenity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PawnController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<PawnController> _logger;

        // Constructor
        public PawnController(IRepository repo, ILogger<PawnController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Methods

        // GET /api/pawns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pawn>>> GetAllPawns()
        {
            IEnumerable<Pawn> pawns;

            try
            {
                pawns = await _repo.GetAllPawnsAsync();
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
