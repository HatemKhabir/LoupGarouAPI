namespace LoupGarou.Model
{
  public class Game
  {
    public string GameId { get; set; }
    public int NumberOfPlayers { get; set; }
    public IList<Player> Players { get; set; }
  }
}
