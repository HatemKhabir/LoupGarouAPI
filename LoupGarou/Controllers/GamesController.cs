using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GamesController : ControllerBase
  {
    private readonly IGameService gameService;
    public GamesController(IGameService gameService)
    {
      this.gameService = gameService;
    }

    // GET: api/<GamesController>
    [HttpGet]
    public async Task<IEnumerable<Game>> Get()
    {
      var allGame = await gameService.GetAllGames();
      return allGame;
    }

    // GET api/<GamesController>/5
    [HttpGet("{id}")]
    public async Task<Game> Get(string id)
    {
      var game = await gameService.GetGame(id);
      return game;
    }

    // POST api/<GamesController>
    [HttpPost]
    public async Task<string> Post([FromBody] int numberOfPlayers)
    {
      string id = await gameService.CreateGame(numberOfPlayers);
      return id;
    }

    // DELETE api/<GamesController>/5
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
       await gameService.DeleteGame(id);
    }
  }
}
