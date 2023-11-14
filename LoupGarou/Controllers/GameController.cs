using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGameService gameService;
    public GameController(IGameService gameService)
    {
      this.gameService = gameService;
    }

    // GET: api/<GameController>
    [HttpGet]
    public async Task<IEnumerable<Game>> Get()
    {
      var allGame = await gameService.GetAllGames();
      return allGame;
    }

    // GET api/<GameController>/5
    [HttpGet("{id}")]
    public async Task<Game> Get(string id)
    {
      var game = await gameService.GetGame(id);
      return game;
    }

    // POST api/<GameController>
    [HttpPost]
    public async Task<string> Post([FromBody] int numberOfPlayers)
    {
      string id = await gameService.CreateGame(numberOfPlayers);
      return id;
    }

    // DELETE api/<GameController>/5
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
       await gameService.DeleteGame(id);
    }
  }
}
