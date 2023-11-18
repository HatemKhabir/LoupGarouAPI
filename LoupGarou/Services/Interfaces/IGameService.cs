using LoupGarou.Model;
using LoupGarou.Model.Requests;

namespace LoupGarou.Services.Interfaces
{
  public interface IGameService
  {
    public Task<Game> CreateGame(CreateGameRequest request);
    public Task<IEnumerable<Game>> GetAllGames();
    public Task<Game> GetGame(string id);
    public Task DeleteGame(string id);
    public Task AddPlayer(Player newPlayer);
    Task RemovePlayer(Player player);
        Task<Game> AssignRolesToPlayers();
    }
}
