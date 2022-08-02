using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using DissonantSerenity.Model;

namespace DissonantSerenity.Data
{
    public class TempData : ITempData
    {
        private readonly ILogger<TempData> _logger;

        // Constructor
        public TempData(ILogger<TempData> logger)
        {
            _logger = logger;
        }


        public async Task<IEnumerable<Pawn>> GetPawnsAsync()
        {
            //List<Pawn> result = new();
            //return result;
            return World.worldPawns;
        }
        
        //Finds pawns based on target name
        public async Task<IEnumerable<Pawn>> FindPawnAsync(string name)
        {
            List<Pawn> result = new();
            foreach (Pawn pawn in World.worldPawns)
            {
                if (pawn.FirstName == name)
                {
                    //pawn.getLocation();
                    result.Add(pawn);
                }
            }

            return result;
        }

        public async Task<IEnumerable<Token>> ObserveLocation(string loc)
        {
            List<Token> result = new();
            foreach (Location location in World.locations)
            {
                Console.WriteLine("Checking if " + loc + " is " + location.name);
                if (location.name == loc)
                {
                    foreach (Token token in location.tokens)
                    {
                        result.Add(token);
                    }
                    break;
                }
            }
            return result;
        }
    }
}
