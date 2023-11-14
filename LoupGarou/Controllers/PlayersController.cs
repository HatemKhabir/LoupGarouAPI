using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoupGarou.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
  private readonly IPlayerService playerService;
  public PlayersController(IPlayerService playerService)
  {
    this.playerService = playerService;
  }

  [HttpPost]
  public async Task<Player> Post([FromBody] CreatePlayerRequest request)
  {
    Player player = await playerService.CreatePlayer(request);
    return player;
  }
  [HttpGet]
  public async Task<IEnumerable<Player>> Get()
  {
    return await playerService.GetAllPlayers();
  }

  [HttpGet("{id}")]
  public async Task<Player> Get(Guid id)
  {
    var player = await playerService.GetPlayer(id);
    return player;
  }

  [HttpDelete("{id}")]
  public async Task Delete(Guid id)
  {
    await playerService.DeletePlayer(id);
  }
}
