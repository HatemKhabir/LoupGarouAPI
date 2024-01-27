using LoupGarou.Model;
using LoupGarou.Model.Requests;

namespace LoupGarou.Services.Interfaces
{
    public interface IGameService
    {
        public Task<Game> CreateGame(CreateGameRequest request);
        public Task<IEnumerable<Game>> GetAllGames();
        public Task<Game> GetGame(Guid id);
        public Task<Game> GetGameByCode(string code);
        public Task DeleteGame(Guid id);
        public Task AddPlayer(Player newPlayer);
        Task RemovePlayer(Player player);
        Task<Game> AssignRolesToPlayers();
    }
}
