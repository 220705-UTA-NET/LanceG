using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DissonantSerenity.Model;

namespace DissonantSerenity.Data
{
    public class PopulateTables
    {
        static Random rand = new Random();
        static List<string> Pawn_Names = new List<string> {"Jonathan De_La_Cruz", "Kadin Campbell", "Annie Arayon-Calosa", "Hau Nguyen", "German Diaz",
            "Brandon Figueredo", "Alejandro Hernandez", "James Beitz", "Abanob Sadek", "Ian Seki", "Iqbal Ahmed", "Brandon Sassano",
            "Daniel Beidelschies", "Derick Xie", "Eunice Decena", "Aurel Npounengnong", "Samuel Jackson", "Ellery De_Jesus", "Rogers Ssozi",
            "Lance Gong", "Arthur Gao", "Jared Green", "Jake Nguyen", "Joseph Boye", "Onandi Stewart", "Andrew Grozdanov", "Richard Hawkins"};
        public static void Main()
        {
            populatePawns("");
        }

        public static void populatePawns(string? key)
        {
            if (key == "" || key == null)
                key = "";
            string connectionString = File.ReadAllText("C:/Revature/ConnectionStrings/DSconnectionString.txt");
            try
            {
                string delete = "DROP TABLE DS.Pawns" + key + ";";
                executeCommand(delete, connectionString);
            }
            catch 
            { }
            string create = "CREATE TABLE DS.Pawns" + key + "(First_Name NVARCHAR(255), Last_Name NVARCHAR(255), x_Coord int, y_Coord int, " +
                "Insanity int, Susceptibility int, Location NVARCHAR(255));";
            executeCommand(create, connectionString);
            foreach(string pawn in Pawn_Names) 
            {
                string[] split = pawn.Split(" ");
                string first = split[0];
                string second = split[1];
                int randX = rand.Next(0, World.maxX);
                int randY = rand.Next(0, 249);//World.currMaxY);
                string location = World.compareCoordinates(randX, randY).name;
                second = second.Replace("_", " ");
                string insert = "INSERT INTO DS.Pawns" + key + " (First_Name, Last_Name, x_Coord, y_Coord, Insanity, Susceptibility, Location) " +
                    "VALUES ('" + first + "', '" + second + "'," + randX + ", " + randY + ", 0, 0, '" + location + "');";
                Console.WriteLine($"{first}: {randX}, {randY}");
                executeCommand(insert, connectionString);
            }
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
