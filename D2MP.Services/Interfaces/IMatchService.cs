using D2MP.Models;
using D2MP.Models.Responses;

namespace D2MP.Services.Interfaces
{
    public interface IMatchService
    {
        Task<DetailedMatch> GetDetailedMatch(string matchId);
        Task<GetMatchHistoryBySequenceNumResponse> GetMatchHistoryBySequenceNumber(string startAtMatchSeqNumber);
    }
}