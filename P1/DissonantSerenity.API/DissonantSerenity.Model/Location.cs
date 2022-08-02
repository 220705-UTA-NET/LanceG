using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissonantSerenity.Model
{
    public class Location
    {
        public string name;
        public int minX { get; set; }
        public int maxX { get; set; }
        public int minY { get; set; }
        public int maxY { get; set; }
        public int threat { get; set; }
        public bool isSleepZone = false;
        public bool isHaunted = false;
        public int hauntDuration = 0;
        public List<Token> tokens { get; set; } //Includes pawns, corpses

        public Location(string name, int minX, int maxX, int minY, int maxY, int threat)
        {
            this.name = name;
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.threat = threat;
            tokens = new List<Token>();
        }

        public int getLocInsanity()
        {
            int sum = 0; int count = 0;
            foreach (Token token in tokens)
            {
                if(token is Pawn) {
                    Pawn pawn = (Pawn)token;
                    sum += pawn.insanity;
                    count++;
                }
            }
            int avg = sum / count;
            return avg;
        }

        public void Tick()
        {
            hauntCheck();
        }

        public void hauntCheck()
        { 

            if (hauntDuration == 0)
            {
                int dice = World.rand.Next(0, 1000);
                if (isHaunted)
                { 
                    Console.WriteLine(this.name + " is no longer Haunted");
                    isHaunted = false;
                }
                if (dice <= this.threat + World.rand.Next(this.threat))
                {
                    isHaunted = true;
                    hauntDuration = 5 + World.rand.Next(this.threat * World.rand.Next(this.threat), this.threat * 5);
                    Console.WriteLine(this.name + " is now Haunted");
                }
            }
            else
                hauntDuration--;   
            
            if (isHaunted)
            {
                List<Pawn> checkKill = new List<Pawn>();
                foreach (Token token in tokens)
                {
                    if (token is Pawn)
                        checkKill.Add((Pawn)token);
                }
                foreach (Pawn pawn in checkKill)
                {
                    if (pawn.insanity > 75)
                    {
                        int dice = World.rand.Next(100);
                        if (dice == 1)
                        {
                            pawn.Kill();
                        }
                    }
                }
            }     
        }
    }
}
