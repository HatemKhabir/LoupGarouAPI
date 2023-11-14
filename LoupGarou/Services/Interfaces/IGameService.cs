using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
  public interface IGameService
  {
    public Task<string> CreateGame(int numberOfPlayers);
    public Task<IEnumerable<Game>> GetAllGames();
    public Task<Game> GetGame(string id);
    public Task DeleteGame(string id);
  }
}
