using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DissonantSerenity.Model;

namespace DissonantSerenity.Data
{
    public interface ITempData
    {
        Task<IEnumerable<Pawn>> GetPawnsAsync();
        Task<IEnumerable<Pawn>> FindPawnAsync(string pawn);
        Task<IEnumerable<Token>> ObserveLocation(string loc);
    }
}
