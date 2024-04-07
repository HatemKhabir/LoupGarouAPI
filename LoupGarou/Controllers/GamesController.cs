using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly int MIN_PLAYERS = 2; //TODO: Move to config
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(CreateGameRequest request)
        {
            if (request.NumberOfPlayers < MIN_PLAYERS) return BadRequest($"You need at least {MIN_PLAYERS} players to start a game");
            var cardsAreValid = ValidateGameCards(request.GameCards);
            if (!cardsAreValid) return BadRequest("Cards are not valid, make sure to give a correct combination of cards.");
            Game game = await gameService.CreateGame(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/Games/" + game.GameId;
            return Created(getUrl, game);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            var allGames = await gameService.GetAllGames();
            if (allGames == null) return NoContent();
            return Ok(allGames);
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> GetById(Guid gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();
            return Ok(game);
        }
        [HttpGet("code/{gameCode}")]
        public async Task<ActionResult<Game>> GetByCode(string gameCode)
        {
            var game = await gameService.GetGameByCode(gameCode);
            if (game == null) return NotFound();
            return Ok(game);
        }

      
        [HttpPut("{gameId}")]
        public async Task<ActionResult<string>> AssignRolesToPlayers(Guid gameId)
        {
            if (gameId == Guid.Empty) return BadRequest("Game ID is invalid");
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound("Game not found");

            Game gameUpdated = await gameService.AssignRolesToPlayers(gameId);
            return gameUpdated != null ? 
                        Ok(gameUpdated) : 
                        BadRequest("An error occured. Please make sure the number of players matches the number of cards");
        }

        [HttpDelete("{gameId}")]
        public async Task<ActionResult> Delete(Guid gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();

            await gameService.DeleteGame(gameId);
            return NoContent();
        }
        
        [HttpDelete("all")]
        public async Task<ActionResult> DeleteAllGames()
        {
            await gameService.DeleteAllGames();
            return NoContent();
        }

        private bool ValidateGameCards(IList<SameCardsGroup> gameCards)
        {
            //There should be at least 2 types of cards in the game
            if (gameCards == null || gameCards.Where(g=>g.NumberOfCards > 0).ToList().Count < 2) return false;

            //Some roles should exist, some roles has a max or min value...
            foreach (var sameCardsGroup in gameCards)
            {
                if (sameCardsGroup.Card == null) return false;  
            }

            return true;
        }
    }
}
