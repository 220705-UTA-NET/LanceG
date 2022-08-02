using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissonantSerenity.Data
{
    public class PopulateTables
    {
        List<string> Pawn_Names = new List<string> {"Jonathan De_La_Cruz", "Kadin Campbell", "Annie Arayon-Calosa", "Hau Nguyen", "German Diaz",
            "Brandon Figueredo", "Alejandro Hernandez", "James Beitz", "Abanob Sadek", "Ian Seki", "Iqbal Ahmed", "Brandon Sassano",
            "Daniel Beidelschies", "Derick Xie", "Eunice Decena", "Aurel Npounengnong", "Samuel Jackson", "Ellery De Jesus", "Rogers Ssozi",
            "Lance Gong", "Arthur Gao", "Jared Green", "Jake Nguyen", "Joseph Boye", "Onandi Stewart", "Andrew Grozdanov", "Richard Hawkins"};
        public static void Main()
        {
        }

        private void populatePawns()
        {
            string delete = "DROP TABLE DS.Pawns;"; ;
            string create = "CREATE TABLE DS.Pawns (First_Name, Last_Name, x_Coord, y_Coord, Insanity);";
            string insert;
            executeCommand(delete, "");
        }
        private static void executeCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
