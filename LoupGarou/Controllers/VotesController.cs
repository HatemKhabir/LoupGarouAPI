using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("api")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly IPlayerService playerService;

        public VotesController(IVoteService service, IPlayerService playerService)
        {
            voteService = service;
            this.playerService = playerService;
        }


        [HttpPost("votes")]
        public async Task<ActionResult<Vote>> Post([FromBody] CreateVoteRequest request)
        {
            if (request == null) return BadRequest($"Please send a valid request");
            if (request.VoterId == Guid.Empty || request.TargetId == Guid.Empty ) 
                return BadRequest($"Please specify both the voter and the target");
            
            var voter = await playerService.GetPlayer(request.VoterId);
            var target = await playerService.GetPlayer(request.TargetId);
            if (voter == null) return BadRequest("The Voter player ID is not correct");
            if (target == null) return BadRequest("The Target player ID is not correct");

            Vote vote = await voteService.CreateVote(request);
            return vote != null ? Ok(vote) : BadRequest("Error while creating the vote");
        }

        [HttpGet("votes")]
        public async Task<ActionResult<Vote>> GetAllVote()
        {
            var allvotes = await voteService.GetAllVotes();
            if (allvotes == null) return NoContent();
            return Ok(allvotes);
        }

        [HttpGet("votingSession/{votingSessionId}/votes")]
        public async Task<ActionResult<Vote>> GetAllSessionVotes(Guid votingSessionId)
        {
            var session= await voteService.GetVotingSession(votingSessionId);
            if (session == null) return NotFound();

            var sessionVotes= await voteService.GetAllSessionVotes(votingSessionId);
            if (sessionVotes == null) return NoContent();
            return Ok(sessionVotes);
        }


        [HttpGet("votes/{voteId}")]
        public async Task<ActionResult<Vote>> GetVote(Guid voteId)
        {
            var vote = await voteService.GetVote(voteId);
            if (vote == null) return NotFound();
            return Ok(vote);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpDelete("votes/{voteId}")]
        public async Task<ActionResult> Delete(Guid voteId)
        {
            var role = await voteService.GetVote(voteId);
            if (role == null) return NotFound();

            await voteService.DeleteVote(voteId);
            return Ok("This should delete a vote matching the specified ID");
        }
    }
}
