using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LoupGarou.Services
{
    public class GameService : IGameService
    {
        private readonly int MIN_PLAYERS = 2; //TODO: Move to config
        private readonly LoupGarouDbContext _loupGarouDbContext;
        private readonly IRoleService roleService;

        public GameService(LoupGarouDbContext loupGarouDbContext, IRoleService roleService)
        {
            _loupGarouDbContext = loupGarouDbContext;
            this.roleService = roleService;
        }

        public async Task<Game> CreateGame(CreateGameRequest request)
        {
            if (request.NumberOfPlayers < MIN_PLAYERS) return null;

            var game = new Game()
            {
                GameId = Guid.NewGuid(),
                GameCode = GetRandomGameCode(),
                NumberOfPlayers = request.NumberOfPlayers,
                CurrentPhase = "config",
                Status = "new",
                Roles = new List<Role>(),
                Players = new List<Player>(),
                Votes = new List<Vote>(),
                Actions = new List<Model.Action>()
            };

            _loupGarouDbContext.Games.Add(game);
            await _loupGarouDbContext.SaveChangesAsync();


            var gameCards = ListOfAllCards(request.GameCards);

            if (gameCards != null)
            {
                foreach (var card in gameCards)
                {
                    //add role to roleService will automotically add it to the Game
                    var role = await roleService.CreateRole(new CreateRoleRequest
                    {
                        GameId = game.GameId,
                        CardId = card.CardId
                    });
                    if (role == null) continue;
                }
            }

            await _loupGarouDbContext.SaveChangesAsync();
            return game;
        }

        private List<Card>? ListOfAllCards(IList<SameCardsGroup> gameCards)
        {
            if (gameCards == null) return null;

            return gameCards
                .Where(group => group != null && group.NumberOfCards > 0) // Filter out any null groups or groups with no cards
                .SelectMany(group => Enumerable.Repeat(group.Card, group.NumberOfCards)) // Create a new sequence of cards for each group
                .ToList(); // Convert the IEnumerable back to a List
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var allGames = await _loupGarouDbContext
              .Games
              .Include(g => g.Players)
              .Include(g => g.Roles)
              .ToListAsync();
            return allGames;
        }

        public async Task<Game> GetGame(Guid id)
        {
            var game = await _loupGarouDbContext
              .Games
              .Include(g => g.Players)
              .Include(g => g.Roles)
              .FirstOrDefaultAsync(g => g.GameId == id);
            return game;
        }
        public async Task<Game> GetGameByCode(string code)
        {
            var game = await _loupGarouDbContext
              .Games
              .Include(g => g.Players)
              .Include(g => g.Roles)
              .FirstOrDefaultAsync(g => g.GameCode == code);
            return game;
        }
        public async Task DeleteGame(Guid id)
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

        private string GetRandomGameCode()
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

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public Task<Game> AssignRolesToPlayers()
        {
            return null;
        }

    }
}
