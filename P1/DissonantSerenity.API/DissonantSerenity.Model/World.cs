using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DissonantSerenity.Model
{
    public static class World
    {
        private static int currMinY = 0;
        public static int currMaxY = 49;
        private static int rowLength = 4;
        public static int maxX = 49 + 50 * (rowLength-1);
        private static int currRow = 0;
        private static int currCol = 0;
        public static System.Timers.Timer? timer;
        public static int minutes = 30;
        public static int hours = 7;
        public static int dayIndex = 0;
        public static bool exit = false;
        public static List<String> days = new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static string time { get; set; }
        public static Random rand = new Random();
        public static List<Location> locations = new List<Location>();
        public static List<Location> sleepZones = new List<Location>();
        public static List<Pawn>? worldPawns { get; set; }
        public static List<Token>? graveyard { get; set; }
        public static void Main()
        {
            worldPawns = new List<Pawn>();
            graveyard = new List<Token>();
            generateMap();
            startSimulation();
        }
        public static void generateMap()
        {
            //new Location(name, min x, max x, min y, max y)
            int currMin = 0;
            int currMax = 49; //exclusive of 50
            int rowLength = 4;
            int colLength = 5;

            Location Dorms = generateProfile("Dorms", 2);
            Dorms.isSleepZone = true; sleepZones.Add(Dorms);
            Location CompLab = generateProfile("Computer Lab", 2);
            Location Forest = generateProfile("Forest", 4);
            Location Suburbs = generateProfile("Suburbs", 2);
            Suburbs.isSleepZone = true; sleepZones.Add(Suburbs);
            Location Alley = generateProfile("Alley", 4);
            Location School = generateProfile("School", 3);
            Location Highway = generateProfile("Highway", 3);
            Location Shrine = generateProfile("Shrine", 4);
            Location Hospital = generateProfile("Hospital", 4);
            Location Apartments = generateProfile("Apartments", 2);
            Apartments.isSleepZone = true; sleepZones.Add(Apartments);
            Location Mall = generateProfile("Mall", 1);
            Location Trails = generateProfile("Trails", 4);
            Location Warehouse = generateProfile("Warehouse", 4);
            Location Pond = generateProfile("Pond", 3);
            Location Tunnels = generateProfile("Tunnels", 4);
            Location Hotel = generateProfile("Hotel", 2);
            Hotel.isSleepZone = true; sleepZones.Add(Hotel);
            Location Docks = generateProfile("Docks", 3);
            Location Downtown = generateProfile("Downtown", 3);
            Downtown.isSleepZone = true; sleepZones.Add(Downtown);
            Location Train = generateProfile("Train Station", 4);
            Location Site = generateProfile("Abandoned Site", 5);

        }

        public static Location generateProfile(string name, int threat)
        {
            int colMin = 50 * currCol;
            int colMax = 49 + 50 * currCol;
            currMinY = 50 * currRow;
            currMaxY = 49 + 50 * currRow;
            if (currCol >= rowLength - 1)
            {
                currCol = 0;
                currRow++;
            }
            else
                currCol++;

            Console.WriteLine("Generated " + name + " at (" + colMin + "," + colMax + ") (" + currMinY + "," + currMaxY + ")");
            Location newLoc = new Location(name, colMin, colMax, currMinY, currMaxY, threat);
            locations.Add(newLoc);
            return newLoc;
        }

        public static Location compareCoordinates(int x, int y) {
            //Console.WriteLine($"Searching {x}, {y}");
            if (locations.Count == 0)
                generateMap();
            foreach (Location loc in locations)
            {
                //Console.WriteLine($"Checking {loc.name}, ({loc.minX}-{loc.maxX}) ({loc.minY}-{loc.maxY})");
                if (x >= loc.minX && x <= loc.maxX) {
                    if (y >= loc.minY && y <= loc.maxY)
                    {
                        //Console.WriteLine($"{loc.name} found match");
                        return loc;
                    }
                }
            }
            return null;
        }

        public static void startSimulation()
        {
            startClock();
            /*while (!exit)
            { 
                Console.ReadLine();
            }
            timer.Stop();
            timer.Dispose();*/
        }
        public static void startClock()
        {
            // Create a timer with interval 1000 per sec
            dayIndex = 0; hours = 0; minutes = 0;
            if (timer == null)
            {
                //timer = new System.Timers.Timer(1);
                timer = new System.Timers.Timer(100);
                //timer = new System.Timers.Timer(1000);
                // Event Trigger
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
            }
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            bool hourPassed = false;
            //Event ticks time by 10-13 minute intervals
            minutes += rand.Next(10, 13);
            if (minutes >= 60)
            {
                hours++;
                hourPassed = true;
                minutes -= 60;
            }
            if (hours >= 24)
            {
                dayIndex++;
                hours -= 24;
            }
            if (dayIndex > 6)
            {
                dayIndex = 0;
            }
            string day = days[dayIndex];
            string cycle = "";
            int hourDisplay = 0;

            if (hours < 12)
            {
                cycle = "am";
                hourDisplay = hours;
            }
            else
            {
                cycle = "pm";
                hourDisplay = hours - 12;
            }

            if(hourDisplay == 0)
            {
                hourDisplay = 12;
            }

            time = $"{day}  {hourDisplay:D2}:{minutes:D2} {cycle}";
            if (hourPassed)
                Console.WriteLine("\n" + $"{day}  {hourDisplay:D2}:00 {cycle}");
            /*Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}, triggering 15 minute increase: new minutes {1}",
                              e.SignalTime, minutes);*/
            GameHandler.tick();
        }
    }
}
