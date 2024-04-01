using System.Text.Json.Serialization;

namespace LoupGarou.Model
{
    public class VotingSession
    {
        public Guid VotingSessionId { get; set; }
        public string VotingSessionType { get; set; } = string.Empty;
        public int ExpectedVotesCount { get; set; }
        public IList<Vote> Votes{ get; set; } = new List<Vote>();
        public bool IsCompleted { get; set; } = false;
        public Guid Result { get; set; }
        public Guid GameId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
       
        [JsonIgnore]
        public Game? Game { get; set; }
    }
}
