using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissonantSerenity.Model
{
    public class Corpse : Token
    {
        public override string? FirstName { get; }
        public override string? LastName { get; }
        public override string? status { get; set; }
        public string timeOfDeath { get; set; }
        public string locOfDeath { get; set; }

        public Corpse(string FirstName, string LastName, string timeOfDeath, string locOfDeath)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.timeOfDeath = timeOfDeath;
            this.locOfDeath = locOfDeath;
            this.status = "Died on " + timeOfDeath;
        }
    }
}
