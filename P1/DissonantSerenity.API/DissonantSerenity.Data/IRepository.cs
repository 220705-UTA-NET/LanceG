using DissonantSerenity.Model;

namespace DissonantSerenity.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Pawn>> LoadPawnsAsync(string? key);
    }
}
