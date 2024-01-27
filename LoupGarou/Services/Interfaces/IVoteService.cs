using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(string request);
        Task<IEnumerable<Vote>> GetAllVotes();
        Task<Vote> GetVote(Guid voteId);
        Task DeleteVote(Guid voteId);
    }
}
