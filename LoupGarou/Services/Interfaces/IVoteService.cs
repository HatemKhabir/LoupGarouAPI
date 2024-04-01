using LoupGarou.Model;
using LoupGarou.Model.Requests;

namespace LoupGarou.Services.Interfaces
{
    public interface IVoteService
    {
        Task<VotingSession> CreateVotingSession(CreateVotingSessionRequest request);
        Task<IEnumerable<VotingSession>> GetAllVotingSessions();
        Task<VotingSession> GetVotingSession(Guid votingSessionId);
        
        Task<Vote> CreateVote(CreateVoteRequest request);
        Task<IEnumerable<Vote>> GetAllVotes();
        Task<IEnumerable<Vote>> GetAllSessionVotes(Guid votingSessionId);
        Task<Vote> GetVote(Guid voteId);
        Task DeleteVote(Guid voteId);
        Task<VotingSession> GetGameCurrentVotingSession(Guid gameId);
        Task SetVotingSessionCompleted(VotingSession session);
    }
}
