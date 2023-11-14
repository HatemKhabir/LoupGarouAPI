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
  public async Task<ActionResult<Player>> Post([FromBody] CreatePlayerRequest request)
  {
    Player player = await playerService.CreatePlayer(request);
    
    if (player == null) return BadRequest();

    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
    var getUrl = baseUrl + "/api/Players/" + player.PlayerId;
    return Created(getUrl, player);
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Player>>> Get()
  {
    return Ok(await playerService.GetAllPlayers());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Player>> Get(Guid id)
  {
    var player = await playerService.GetPlayer(id);
    if(player == null) return NotFound();
    return Ok(player);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var player = await playerService.GetPlayer(id);
    if(player == null) return NotFound();

    await playerService.DeletePlayer(id);
    return NoContent();
  }
}
