using Azure.Core;
using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    if(request == null || request.PlayerName.IsNullOrEmpty() || request.GameCode.IsNullOrEmpty()) return null;
    
    var game = await gameService.GetGameByCode(request.GameCode);
    if(game == null) return null;

    Player newPlayer = new Player()
    {
      PlayerId = Guid.NewGuid(),
      GameId = game.GameId,
      Name = request.PlayerName,
      Status="created"
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

    public async Task<IEnumerable<Player>> GetGamePlayers(Guid gameId)
    {
        var game = await gameService.GetGame(gameId);
        if (game == null) return null;
        return game.Players;

    }

    public async Task<Player> UpdatePlayer(Guid id,UpdatePlayerRequest updatedPlayer)
    {
        var player = await GetPlayer(id);
        if (player==null) 
            return null;
        if (updatedPlayer.IsProtected!=null)
        {
            player.IsProtected = updatedPlayer.IsProtected;
        }
		if (updatedPlayer.IsDead != null)
		{
			player.IsDead = updatedPlayer.IsDead;
		}
		if (updatedPlayer.IsLover != null)
		{
			player.IsLover = updatedPlayer.IsLover;
		}
        loupGarouDbContext.Players.Update(player);
        await loupGarouDbContext.SaveChangesAsync();
        return player;
	}
}
