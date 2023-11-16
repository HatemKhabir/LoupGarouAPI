namespace LoupGarou.Model.Requests
{
  public class CreateGameRequest
  {
        public int NumberOfPlayers { get; set; }
        public IList<Character> Characters { get; set; } 
    }
}
