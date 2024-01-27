namespace LoupGarou.Model
{
    public class Game
    {
        public string GameId { get; set; }
        public int NumberOfPlayers { get; set; }
        public IList<Player> Players { get; set; }
        public IList<Role> Roles { get; set; }
        public string CurrentPhase { get; set; }
        public string Status { get; set; }
    }
}
