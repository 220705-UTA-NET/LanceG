using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissonantSerenity.Model
{
    public abstract class Token
    {
        public abstract string? FirstName { get; }
        public abstract string? LastName { get; }
        public abstract string? status { get; set; }
    }
}
