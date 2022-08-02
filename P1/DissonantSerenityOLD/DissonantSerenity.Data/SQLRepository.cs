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
        public async Task<IEnumerable<Pawn>> GetAllPawnsAsync()
        {
            List<Pawn> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT First_Name, Last_Name, x_Coord, y_Coord, Insanity FROM DS.Pawns;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string first = reader.GetString(0);
                string last = reader.GetString(1);
                int x = reader.GetInt32(2);
                int y = reader.GetInt32(3);
                int insanity = reader.GetInt32(4);

                Pawn newPawn = new Pawn(first, last);
                result.Add(newPawn);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllPawnAsync, returned {0} results", result.Count);

            return result;
        }

    }
}
