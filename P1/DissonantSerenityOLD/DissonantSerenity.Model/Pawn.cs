using System.Threading;

namespace DissonantSerenity.Model
{
    public class Pawn
    {
        public string FirstName;
        public string LastName;
        public int x;
        public int y;
        public int insanity;
        public Location? target;
        public List<string>? traits;

        public Pawn(string FirstName, string LastName/*, int x, int y, int insanity*/)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.x = 0;
            this.y = 0;
            this.insanity = 0;
        }
    }
}