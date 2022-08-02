using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using DissonantSerenity.Model;

namespace DissonantSerenity.Data
{
    public class SQLRepository : IRepository
    {
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<SQLRepository> _logger;

        // Constructor
        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        // Methods
        public async Task<IEnumerable<Pawn>> LoadPawnsAsync(string? key)
        {

            //no call to update that suggests loading a save state. Needs revision
            List<Pawn> result = new();
            if (key == null)
                key = "";
            try
            {

                using SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string cmdText = "SELECT First_Name, Last_Name, x_Coord, y_Coord, Insanity, Susceptibility, Location FROM DS.Pawns" + key + ";";
                using SqlCommand cmd = new(cmdText, connection);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string first = reader.GetString(0);
                    string last = reader.GetString(1);
                    int x = reader.GetInt32(2);
                    int y = reader.GetInt32(3);
                    int insanity = reader.GetInt32(4);
                    int susceptibility = reader.GetInt32(5);
                    string location = reader.GetString(6);

                    Pawn newPawn = new Pawn(first, last, x, y, insanity, susceptibility, location);
                    //Console.WriteLine(newPawn.FirstName + " is in the " + newPawn.getLocation());
                    //Pawn insanity increases from user interference
                    //_logger.LogWarning(newPawn.FirstName + " feels a strange presence looming over them");
                    result.Add(newPawn);
                }

                await connection.CloseAsync();

                //_logger.LogInformation("Executed GetAllPawnAsync, returned {0} results", result.Count);
                World.worldPawns = result;
                return result;
            }
            catch
            {
                PopulateTables.populatePawns(key);
                _logger.LogInformation("Created new save: " + key);
                return await LoadPawnsAsync(key);
            }
            _logger.LogCritical("Failed to load key");
            return result;
        }
    }
}
