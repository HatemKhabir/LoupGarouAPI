using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("TODO/api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService voteService;

        public VotesController(IVoteService service)
        {
            voteService = service;
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
            Vote vote = await voteService.CreateVote(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/roles/" + vote.VoterId;
            return Ok("This should create a new vote");
            //return Created(getUrl, role);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            var allroles = await voteService.GetAllVotes();
            if (allroles == null) return NoContent();
            return Ok("This should return all votes in the DB");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet("{voteId}")]
        public async Task<ActionResult<Game>> Get(Guid voteId)
        {
            var role = await voteService.GetVote(voteId);
            if (role == null) return NotFound();
            return Ok("This should return the vote");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{voteId}")]
        public async Task<ActionResult> Delete(Guid voteId)
        {
            var role = await voteService.GetVote(voteId);
            if (role == null) return NotFound();

            await voteService.DeleteVote(voteId);
            return Ok("This should delete a vote matching the specified ID");
        }
    }
}
