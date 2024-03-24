using System.Data;
using System.Text.Json.Serialization;

namespace LoupGarou.Model
{
    public class Vote
    {
        public Guid VoteId { get; set; }
        public Guid VoterId { get; set; }
        public Guid TargetId { get; set; }
        public Guid VotingSessionId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        
        [JsonIgnore]
        public VotingSession VotingSession { get; set; }
    }
}
