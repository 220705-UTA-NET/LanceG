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
        private readonly ITempData _repo;
        private readonly ILogger<PawnController> _logger;

        // Constructor
        public PawnController(ITempData repo, ILogger<PawnController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Methods

        // GET /api/pawns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pawn>>> GetPawns(string? name = null)
        {
            IEnumerable<Pawn> pawns;
            if (name != null)
            {
                _logger.LogInformation("A stalking presence searches for " + name);
                try
                {
                    pawns = await _repo.FindPawnAsync(name);

                }
                catch (Exception e)
                {
                    _logger.LogInformation("The presence drifts away in unrest");
                    return StatusCode(500);
                }
            }
            else
            {
                try
                {
                    pawns = await _repo.GetPawnsAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    return StatusCode(500);
                }
            }
            return pawns.ToList();
        }
    }
}
