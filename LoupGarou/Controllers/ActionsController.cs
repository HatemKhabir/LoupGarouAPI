using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services;
using LoupGarou.Services.Interfaces;
using LoupGarou.Utils;
using Microsoft.AspNetCore.Mvc;
using Action = LoupGarou.Model.Action;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionService _actionService;
        private readonly IPlayerService _playerService;
        private ILogger<ActionsController> logger;
        public ActionsController(IActionService service, IPlayerService playerService,ILogger<ActionsController> logger)
        {
            _actionService = service;
            _playerService = playerService;
            this.logger = logger;
        }

   
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateActionRequest request)
        {
            if (request == null) return BadRequest($"Please send a valid request");
            if (request.PlayerId == null || request.TargetId == null)
                return BadRequest("Please Specify Your or the TargetId !");
            var voter = await _playerService.GetPlayer(request.PlayerId);
            var target = await _playerService.GetPlayer(request.TargetId);
			if (voter == null) return BadRequest("The Voter player ID is not correct");
			if (target == null) return BadRequest("The Target player ID is not correct");
            switch (request.ActionType)
            {case Actions.Kill:
                    var updatedPlayer =await _playerService.UpdatePlayer(request.TargetId, new UpdatePlayerRequest { IsDead = true });
                    logger.LogInformation("Updated player object", updatedPlayer);
                    break;
             case Actions.Revive:
					updatedPlayer = await _playerService.UpdatePlayer(request.TargetId, new UpdatePlayerRequest { IsDead = false });
					logger.LogInformation("Updated player object", updatedPlayer);

					break;
            case Actions.Protect:
					updatedPlayer=await _playerService.UpdatePlayer(request.TargetId, new UpdatePlayerRequest { IsProtected = true });
					logger.LogInformation("Updated player object", updatedPlayer);

					break;
			}
			Action action = await _actionService.CreateAction(request);
			logger.LogInformation("Created action", action);
			var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/actions/" + action.ActionId;
            return Ok(action);
            //return Created(getUrl, role);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            var allroles = await _actionService.GetAllActions();
            if (allroles == null) return NoContent();
            return Ok(allroles);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ActionId}")]
        public async Task<ActionResult<Game>> Get(Guid actionId)
        {
            var role = await _actionService.GetAction(actionId);
            if (role == null) return NotFound();
            return Ok("This should return the Action");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{actionId}")]
        public async Task<ActionResult> Delete(Guid actionId)
        {
            var role = await _actionService.GetAction(actionId);
            if (role == null) return NotFound();

            await _actionService.DeleteAction(actionId);
            return Ok("This should delete a Action matching the specified ID");
        }
    }
}
