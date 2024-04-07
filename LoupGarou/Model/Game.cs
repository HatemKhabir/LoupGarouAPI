namespace LoupGarou.Model
{
    public class Game
    {
        public Guid GameId { get; set; }
        public string GameCode { get; set; }
        public int NumberOfPlayers { get; set; }
        public string CurrentPhase { get; set; }
        public string Status { get; set; }
        public IList<Player> Players { get; set; }
        public IList<Role> Roles { get; set; }
        public IList<VotingSession> VotingSessions { get; set; } 
        public IList<Action> Actions{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    // game currentPhase : join-lobby , assign-roles, cheif-vote, close-eyes, protector, wolves..
    // game status: new, started, finished
}
