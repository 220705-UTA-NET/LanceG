using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissonantSerenity.Model
{
    public class GameHandler
    {
        public static void tick()
        {
            foreach(Pawn pawn in World.worldPawns)
            {
                pawn.Act();
            }
            foreach (Location loc in World.locations)
            {
                loc.Tick();
            }
        }
    }
}
