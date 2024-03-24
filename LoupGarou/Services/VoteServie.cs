using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoupGarou.Services
{
    public class VoteServie : IVoteService
    {
        private readonly LoupGarouDbContext loupGarouDbContext;
        private readonly IGameService gameService;

        public VoteServie(LoupGarouDbContext loupGarouDbContext, IGameService gameService)
        {
            this.loupGarouDbContext = loupGarouDbContext;
            this.gameService = gameService;
        }


        public async Task<VotingSession?> CreateVotingSession(CreateVotingSessionRequest request)
        {
            if (request == null || request.VotingSessionType.IsNullOrEmpty() || request.GameId == Guid.Empty)
                return null;

            Game game = await gameService.GetGame(request.GameId);
            if (game == null) return null;

            VotingSession session = new VotingSession()
            {
                VotingSessionId = new Guid(),
                GameId = request.GameId,
                Game = game,
                VotingSessionType = request.VotingSessionType,
                Votes = new List<Vote>(),
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            loupGarouDbContext.VotingSessions.Add(session);
            await loupGarouDbContext.SaveChangesAsync();
            return session;
        }

        public async Task<IEnumerable<VotingSession>> GetAllVotingSessions()
        {
            var sessions = await loupGarouDbContext
               .VotingSessions
               .Include(s => s.Votes)
               .ToListAsync();

            return sessions;
        }

        public async Task<VotingSession> GetVotingSession(Guid votingSessionId)
        {
            VotingSession? session = await loupGarouDbContext
                .VotingSessions
                .Include(s => s.Votes)
                .FirstOrDefaultAsync(s => s.VotingSessionId == votingSessionId);
            return session;
        }

        public async Task<IEnumerable<Vote>?> GetAllSessionVotes(Guid votingSessionId)
        {

            var session = await GetVotingSession(votingSessionId);
            if (session == null) return null;

            var votes = session.Votes;
            if (votes.IsNullOrEmpty()) return null;
            return session.Votes;
        }

        public async Task<Vote> CreateVote(CreateVoteRequest request)
        {
            if (request == null) return null;

            var session = await GetVotingSession(request.VotingSessionId);
            if (session == null) return null;

            Vote vote = new Vote()
            {
                VoteId = new Guid(),
                VoterId = request.VoterId,
                TargetId = request.TargetId,
                VotingSession = session,
                VotingSessionId = request.VotingSessionId,
                CreatedAt = DateTime.UtcNow
            };

            loupGarouDbContext.Votes.Add(vote);
            await loupGarouDbContext.SaveChangesAsync();
            return vote;
        }

        public async Task<IEnumerable<Vote>> GetAllVotes()
        {
            var allVotes = await loupGarouDbContext.Votes.ToListAsync();
            return allVotes;     
        }


        public async Task<Vote> GetVote(Guid voteId)
        {
            var vote = await loupGarouDbContext.Votes.FindAsync(voteId);
            return vote;
        }

        public Task DeleteVote(Guid voteId)
        {
            throw new NotImplementedException();
        }
    }
}
