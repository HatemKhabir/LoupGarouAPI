using LoupGarou.Model;
using LoupGarou.Model.Requests;

namespace LoupGarou.Services.Interfaces
{
  public interface IPlayerService
  {
    public Task<Player> CreatePlayer(CreatePlayerRequest request);
    public Task<IEnumerable<Player>> GetAllPlayers();
    public Task<Player> GetPlayer(Guid id);
    public Task DeletePlayer(Guid id);
  }
}
