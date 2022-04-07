using D2MP.Models;

namespace D2MP.Data.Interfaces
{
    public interface IPartialMatchResultRepository
    {
        Task<int> Count();
        Task<long> GetLastMatchSeqId();
        Task<PartialMatchResult[]> GetAll();
        Task Insert(PartialMatchResult match);
        Task Insert(long processedMatchSeqId);
    }
}
