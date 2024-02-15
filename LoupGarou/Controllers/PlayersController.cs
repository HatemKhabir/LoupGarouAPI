using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoupGarou.Controllers;
[Route("api")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService playerService;
    public PlayersController(IPlayerService playerService)
    {
        this.playerService = playerService;
    }

    [HttpPost("players")]
    public async Task<ActionResult<Player>> Post([FromBody] CreatePlayerRequest request)
    {
        Player player = await playerService.CreatePlayer(request);
        if (player == null) return BadRequest();

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var getUrl = baseUrl + "/api/Players/" + player.PlayerId;
        return Created(getUrl, player);
    }

    [HttpGet("players")]
    public async Task<ActionResult<IEnumerable<Player>>> GetAll()
    {
        var players = await playerService.GetAllPlayers();
        if(players == null) return BadRequest();
        return Ok(players);
    }

    [HttpGet("games/{gameId}/players")]
    public async Task<ActionResult<IEnumerable<Player>>> GetGamePlayers(Guid gameId)
    {
        var players = await playerService.GetGamePlayers(gameId);
        if (players == null) return NotFound();
        return Ok(players);
    }
    
    [HttpGet("players/{id}")]
    public async Task<ActionResult<Player>> GetById(Guid id)
    {
        var player = await playerService.GetPlayer(id);
        if (player == null) return NotFound();
        return Ok(player);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpPut("players/{playerId}/TODO")]
    public async Task<ActionResult<string>> Put(Guid playerId)
    {
        var player= await playerService.GetPlayer(playerId);
        if (player == null) return NotFound();

        Game playerUpdated = await playerService.UpdatePlayer();
        return Ok("This request should the player state");
    }

    [HttpDelete("players/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var player = await playerService.GetPlayer(id);
        if (player == null) return NotFound();

        await playerService.DeletePlayer(id);
        return NoContent();
    }
}
