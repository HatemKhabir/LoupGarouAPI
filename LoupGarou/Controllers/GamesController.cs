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

        // POST api/<GamesController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateGameRequest request)
        {
            if (request.NumberOfPlayers < MIN_PLAYERS) return BadRequest($"You need at least {MIN_PLAYERS} players to start a game");
            Game game = await gameService.CreateGame(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/Games/" + game.GameId;
            return Created(getUrl, game);
        }

        // GET: api/<GamesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            var allGames = await gameService.GetAllGames();
            if (allGames == null) return NoContent();
            return Ok(allGames);
        }

        // GET api/<GamesController>/5
        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> Get(string gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();
            return Ok(game);
        }

        // GET api/<GamesController>/5/players
        [HttpGet("{gameId}/players")]
        public async Task<ActionResult<IEnumerable<Player>>> GetGamePlayers(string gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();
            return Ok(game.Players);
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{gameId}")]
        public async Task<ActionResult> Delete(string gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();

            await gameService.DeleteGame(gameId);
            return NoContent();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpPut("{gameId}/AssignRoles/TODO")]
        public async Task<ActionResult<string>> PostAssignRoles(string gameId)
        {
            var game = await gameService.GetGame(gameId);
            if (game == null) return NotFound();

            Game gameUpdated = await gameService.AssignRolesToPlayers();
            return Ok("This request should assign a character to each one of this game players");
        }

    }
}
