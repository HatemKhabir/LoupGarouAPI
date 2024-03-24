namespace LoupGarou.Model.Requests
{
    public class CreateVoteRequest
    {
        public Guid VotingSessionId { get; set; }
        public Guid VoterId { get; set; }
        public Guid TargetId { get; set; }
    }
}
