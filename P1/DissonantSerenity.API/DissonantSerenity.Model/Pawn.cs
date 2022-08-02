using System.Threading;

namespace DissonantSerenity.Model
{
    public class Pawn : Token
    {
        public override string? FirstName { get; }
        public override string? LastName { get; }
        public int x { get; set; }
        public int y { get; set; }
        public int insanity { get; set; }
        public int susceptibility { get; set; }
        public Location currLocation;
        public Location targetLocation;
        public string locToString { get; set; }
        private int turnsToRoam = 0; private int xDir; private int yDir; private int boredom;
        private bool checkSleep = false;
        private int sleepMeter = 30;
        public override string? status { get; set; }
        /*public Location? target;
        public List<string>? traits;*/

        public Pawn() { }

        public Pawn(string FirstName, string LastName, int x, int y, int insanity, int susceptibility, string locString)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.x = x;
            this.y = y;
            this.insanity = insanity;
            this.susceptibility = susceptibility;
            this.locToString = this.getLocation();
            this.status = status;
        }

        public string getLocation()
        {
            Location previousLoc = this.currLocation;
            this.currLocation = World.compareCoordinates(this.x, this.y);
            if (previousLoc == null)
            {
                this.currLocation.tokens.Add(this);
                return this.currLocation.name;
            }

            if (previousLoc != this.currLocation)
            {
                previousLoc.tokens.Remove(this);
                this.currLocation.tokens.Add(this);
                Console.WriteLine(this.FirstName + " moved from " + previousLoc.name + " to " + this.currLocation.name);

                //Triggers Chance to idle upon moving to new location
                this.boredom = 0;
                int dice = World.rand.Next(100);
                if (dice < 30)
                {
                    this.turnsToRoam -= World.rand.Next(2, 5);
                    this.xDir = 0;
                    this.yDir = 0;
                }
                else if (dice < 40)
                {
                    this.turnsToRoam -= World.rand.Next(0, 3);
                }
                else if (dice < 50)
                {
                    this.turnsToRoam += World.rand.Next(3, 5);
                }
            }
            else
            {
                this.boredom++;
            }
            return this.currLocation.name;
        }

        public void Act()
        {
            this.calcInsanity();
            this.move();
        }

        public void calcInsanity()
        {
            int insanityAdd = this.currLocation.threat;
            int susceptAdd = this.currLocation.threat;
            int timeCheck = World.hours;
            int locMalus = this.currLocation.getLocInsanity();

            //check daytime/nightime for base insanity without modifiers
            if (timeCheck >= 7 && timeCheck <= 20 && !this.currLocation.isHaunted)
            {

                if (susceptibility < 40)
                {
                    insanityAdd -= World.rand.Next(2, 7 - this.currLocation.threat);
                    susceptAdd -= World.rand.Next(1, 6 - this.currLocation.threat);
                }
                else if (susceptibility < 70)
                {
                    insanityAdd -= World.rand.Next(1, 6 - this.currLocation.threat);
                    susceptAdd -= World.rand.Next(2, 7 - this.currLocation.threat);
                }
                else
                    susceptAdd -= World.rand.Next(5 - this.currLocation.threat, 6 - this.currLocation.threat);
            }
            else
            {
                if (this.currLocation.isSleepZone && !this.currLocation.isHaunted)
                {
                    insanityAdd -= World.rand.Next(1, this.currLocation.threat + 1);
                    susceptAdd -= World.rand.Next(5 - this.currLocation.threat, 8 - this.currLocation.threat);
                }
                else if (this.currLocation.isHaunted)
                {
                    insanityAdd += this.currLocation.threat * 3 / 4;
                    susceptAdd += 3;
                }
                susceptAdd += 1;
            }

            if (this.insanity > locMalus)
            {
                insanityAdd -= 1;
            }
            if (this.insanity < locMalus)
            {
                insanityAdd += 1;
            }
            if (this.currLocation.tokens.Count == 1)
            {
                insanityAdd += insanityAdd / 4;
                susceptAdd += susceptAdd / 2;
            }

            if (this.currLocation.isHaunted)
            {
                insanityAdd += insanityAdd * 3 / 4;
                susceptAdd += susceptAdd * 3 / 4;
            }

            if (this.insanity > 75)
            {
                insanityAdd = insanityAdd * 3 / 4;
            }

            this.insanity += insanityAdd;
            this.susceptibility += susceptAdd;

            if (this.insanity < 0)
                this.insanity = 0;
            if (this.insanity > 100)
                this.insanity = 100;
            if (this.susceptibility < 0)
                this.susceptibility = 0;
            if (this.susceptibility > 100)
                this.susceptibility = 100;

        }

        public void move()
        {
            int sleepRoll = 0;
            if (World.hours >= 20 && !this.checkSleep)
            {
                sleepRoll = 3 + World.hours - 20;
                sleepMeter = 1;
            }
            else if (World.hours >= 0 && World.hours <= 7 && !this.checkSleep)
            {
                sleepRoll = 7 - World.hours;
            }
            if (this.sleepMeter > 0)
            {
                int dice = World.rand.Next(100);
                if (dice < sleepRoll || this.checkSleep)
                {
                    this.status = "Sleeping";
                    this.sleep();
                    return;
                }
            }

            this.checkSleep = false;
            if (this.turnsToRoam <= 0)
            {
                this.xDir = World.rand.Next(-1, 1);
                this.yDir = World.rand.Next(-1, 1);
                this.turnsToRoam = World.rand.Next(3 + this.boredom, 10 + this.boredom);
            }
            else
            {
                this.turnsToRoam--;
            }
                
            if(this.xDir == 0)
                this.status = "Idling";
            else
                this.status = "Wandering";
            this.x += World.rand.Next(0, 3) * this.xDir;
            this.y += World.rand.Next(0, 3) * this.yDir;

            if (this.x < 0)
            { 
                this.x = 0;
                this.xDir = 1;
            }
            if (this.x > World.maxX)
            { 
                this.x = World.maxX;
                this.xDir = -1;
            }
            if (this.y < 0)
            { 
                this.y = 0;
                this.yDir = 1;
            }
            if (this.y > World.currMaxY)
            { 
                this.y = World.currMaxY;
                this.yDir = -1;
            }
            this.locToString = this.getLocation();
        }

        public void sleep()
        {
            if (sleepMeter <= 0)
            {
                return;
            }
            Location? closestTarget = null;
            int closestDistance = 0;
            if (!this.checkSleep)
            {
                foreach (Location loc in World.sleepZones)
                {
                    if (loc.isSleepZone)
                    {
                        int tempDist = getDistance(loc);
                        if (closestTarget == null)
                        {
                            closestTarget = loc;
                            closestDistance = tempDist;
                        }
                        else if (tempDist < closestDistance)
                        {
                            closestTarget = loc;
                            closestDistance = tempDist;
                        }
                    }
                }
                this.targetLocation = closestTarget;
                Console.WriteLine($"{this.FirstName} is returning to {closestTarget.name} to sleep");
                this.sleepMeter = World.rand.Next(35, 50);
                this.checkSleep = true;
            }
            if (this.currLocation != this.targetLocation)
            {
                this.moveTowards(this.targetLocation);
            }
            this.sleepMeter--;
        }

        public int getDistance(Location loc)
        {
            return (int)Math.Pow(Math.Pow(this.x - loc.minX - 25, 2) + Math.Pow(this.y - loc.minY - 25, 2), 0.5);
        }
        public void moveTowards(Location loc)
        {
            if (this.x < loc.minX + 25)
                this.x += World.rand.Next(0, 5);
            if (this.x > loc.minX + 25)
                this.x -= World.rand.Next(0, 5);
            if (this.y < loc.minY + 25)
                this.y += World.rand.Next(0, 5);
            if (this.y > loc.minY + 25)
                this.y -= World.rand.Next(0, 5);
            this.locToString = this.getLocation();
        }

        public void Kill()
        {
            Console.WriteLine($"{this.FirstName} died in {this.currLocation.name}");
            this.currLocation.tokens.Remove(this);
            World.worldPawns.Remove(this);
            Corpse corpse = new Corpse(this.FirstName, this.LastName, World.time, this.currLocation.name);
            this.currLocation.tokens.Add(corpse);
            World.graveyard.Add(corpse);
        }
    }
}