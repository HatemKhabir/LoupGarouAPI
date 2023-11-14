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

    public async Task<string> CreateGame(int numberOfPlayers)
    {
      var game = new Game()
      {
        GameID = GetRandomGameId(),
        NumberOfPlayers = numberOfPlayers
      };
      _loupGarouDbContext.Games.Add(game);
      await _loupGarouDbContext.SaveChangesAsync();
      return game.GameID;
    }
    public async Task<IEnumerable<Game>> GetAllGames()
    {
      var allGames = await _loupGarouDbContext.Games.ToListAsync();
      return allGames;
    }

    public async Task<Game> GetGame(string id)
    {
      var game = await _loupGarouDbContext.Games.FindAsync(id);
      return game;
    }
    public async Task DeleteGame(string id)
    {
      var game = await _loupGarouDbContext.Games.FindAsync(id);
      _loupGarouDbContext.Games.Remove(game);
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
