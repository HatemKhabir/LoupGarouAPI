using System.Data;

namespace LoupGarou.Model
{
    public class Vote
    {
        public Guid VoteId { get; set; }
        public Guid VoterId { get; set; }
        public Guid TargetId { get; set; }
        public Guid GameId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public string Result { get; set; }
    }
}
