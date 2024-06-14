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
    private readonly IGameService gameService;

    public PlayersController(IPlayerService playerService, IGameService gameService)
    {
        this.playerService = playerService;
        this.gameService = gameService;
    }

    [HttpPost("players")]
    public async Task<ActionResult<Player>> Post([FromBody] CreatePlayerRequest request)
    {
        if (request == null) return BadRequest("The request didn't reach the server");
        
        Game game = await gameService.GetGameByCode(request.GameCode);
        if (game == null) return NotFound("The game code is not correct");

        if (game.Players.Count == game.NumberOfPlayers) return BadRequest("The game is full");

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
    public async Task<Player> Put(Guid playerId)
    {
        throw new NotImplementedException();
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
