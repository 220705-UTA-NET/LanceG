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
        public int minInt;
        public int maxInt;

        public Location(string name, int minInt, int maxInt)
        {
            this.name = name;
            this.minInt = minInt;
            this.maxInt = maxInt;
        }
    }
}
