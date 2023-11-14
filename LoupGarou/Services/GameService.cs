using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LoupGarou.Services
{
  public class GameService : IGameService
  {
    private readonly LoupGarouDbContext _loupGarouDbContext;

    public GameService(LoupGarouDbContext loupGarouDbContext)
    {
      _loupGarouDbContext = loupGarouDbContext;
    }

    public async Task<Game> CreateGame(int numberOfPlayers)
    {
      var game = new Game()
      {
        GameId = GetRandomGameId(),
        NumberOfPlayers = numberOfPlayers
      };
      _loupGarouDbContext.Games.Add(game);
      await _loupGarouDbContext.SaveChangesAsync();
      return game;
    }
    public async Task<IEnumerable<Game>> GetAllGames()
    {
      var allGames = await _loupGarouDbContext
        .Games
        .Include(g => g.Players)
        .ToListAsync();
      return allGames;
    }

    public async Task<Game> GetGame(string id)
    {
      var game = await _loupGarouDbContext
        .Games
        .Include(g => g.Players)
        .FirstOrDefaultAsync(g => g.GameId == id);
      return game;
    }
    public async Task DeleteGame(string id)
    {
      var game = await _loupGarouDbContext.Games.FindAsync(id);
      _loupGarouDbContext.Games.Remove(game);
      await _loupGarouDbContext.SaveChangesAsync();
    }

    public async Task AddPlayer(Player newPlayer)
    {
      var game = await GetGame(newPlayer.GameId);
      game.Players.Add(newPlayer);
      _loupGarouDbContext.Games.Update(game);
      await _loupGarouDbContext.SaveChangesAsync();
    }

    public async Task RemovePlayer(Player player)
    {
      var game = await GetGame(player.GameId);
      game.Players.Remove(player);
      _loupGarouDbContext.Games.Update(game);
      await _loupGarouDbContext.SaveChangesAsync();
    }

    private string GetRandomGameId()
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

      // Create a random number generator
      Random random = new Random();

      // Create a string builder to store the result
      StringBuilder builder = new StringBuilder(4);

      // Loop for the number of characters
      for (int i = 0; i < 4; i++)
      {
        // Append a random character from the chars string
        builder.Append(chars[random.Next(chars.Length)]);
      }

      // Return the random string
      return builder.ToString();
    }
  }
}
