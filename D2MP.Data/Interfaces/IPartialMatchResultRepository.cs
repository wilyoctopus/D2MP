using D2MP.Models;

namespace D2MP.Data.Interfaces
{
    public interface IPartialMatchResultRepository
    {
        Task<int> CountAsync();
        Task<long> GetLastMatchSeqIdAsync();
        long GetLastMatchSeqId();
        Task<PartialMatchResult[]> GetAllAsync();
        Task InsertAsync(PartialMatchResult match);
    }
}
