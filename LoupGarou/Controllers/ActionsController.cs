using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Action = LoupGarou.Model.Action;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("TODO/api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionsController(IActionService service)
        {
            _actionService = service;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string request)
        {
            if (request == null) return BadRequest($"Please send a valid request");
            Action action= await _actionService.CreateAction(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/actions/" + action.ActionId;
            return Ok("This should create a new Action");
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
            return Ok("This should return all Actions in the DB");
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
