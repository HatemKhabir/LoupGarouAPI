namespace LoupGarou.Model.Requests
{
    public class CreateVotingSessionRequest
    {
        public string VotingSessionType { get; set; } = string.Empty;
        public int ExpectedVotesCount { get; set; }
        public Guid GameId { get; set; }
    }
}
