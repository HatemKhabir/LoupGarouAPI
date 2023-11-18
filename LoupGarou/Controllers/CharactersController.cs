using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("TODO/api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharactersController(ICharacterService service)
        {
            characterService = service;
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
            Character character = await characterService.CreateCharacter(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/Characters/" + character.CharacterID;
            return Ok("This should create a new character (Loup/ witch/ hunter .. ) and returna unique ID");
            //return Created(getUrl, character);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var allCharacters = await characterService.GetAllCharacters();
            if (allCharacters == null) return NoContent();
            return Ok("This should return all characters in the DB");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> Get(string characterID)
        {
            var character = await characterService.GetCharacter(characterID);
            if (character == null) return NotFound();
            return Ok("This should return the character matching a specified ID (Loup/Salvaddor..)");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{gameId}")]
        public async Task<ActionResult> Delete(string characterID)
        {
            var character = await characterService.GetCharacter(characterID);
            if (character == null) return NotFound();

            await characterService.Deletecharacter(characterID);
            return Ok("This should delete a character matching the specified ID");
        }
    }
}
