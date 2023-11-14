using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoupGarou.Services;
public class PlayerService : IPlayerService
{
  private readonly LoupGarouDbContext loupGarouDbContext;
  private readonly IGameService gameService;

  public PlayerService(LoupGarouDbContext loupGarouDbContext, IGameService gameService)
  {
    this.loupGarouDbContext = loupGarouDbContext;
    this.gameService = gameService;
  }
  public async Task<Player> CreatePlayer(CreatePlayerRequest request)
  {
    Player newPlayer = new Player()
    {
      PlayerId = Guid.NewGuid(),
      GameId = request.GameId,
      Name = request.PlayerName,
    };
    loupGarouDbContext.Players.Add(newPlayer);
    await loupGarouDbContext.SaveChangesAsync();

    await gameService.AddPlayer(newPlayer);

    return newPlayer;
  }

  public async Task<IEnumerable<Player>> GetAllPlayers()
  {
    var allPlayers= await loupGarouDbContext.Players.ToListAsync();
    return allPlayers;
  }

  public async Task<Player> GetPlayer(Guid id)
  {
    var player = await loupGarouDbContext.Players.FindAsync(id);
    return player;
  }
  public async Task DeletePlayer(Guid id)
  {
    var player = await loupGarouDbContext.Players.FindAsync(id);
    loupGarouDbContext.Players.Remove(player);
    await loupGarouDbContext.SaveChangesAsync();

    await gameService.RemovePlayer(player);
  }
}
