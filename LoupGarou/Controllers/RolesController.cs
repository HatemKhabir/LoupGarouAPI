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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService service)
        {
            roleService = service;
        }

        [HttpPost("roles")]
        public async Task<ActionResult<string>> Post([FromBody] CreateRoleRequest request)
        {
            if (request == null) return BadRequest($"Please send a valid request");
            Role role = await roleService.CreateRole(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/roles/" + role.RoleId;
            return Created(getUrl, role);
            //return Ok(role);
        }

        [HttpGet("roles")]
        public async Task<ActionResult<string>> GetAll()
        {
            var allroles = await roleService.GetAllRoles();
            if (allroles == null) return NoContent();
            return Ok(allroles);
        }

        [HttpGet("games/{gameId}/roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetGameRoles(Guid gameId)
        {
            var roles = await roleService.GetGameRoles(gameId);
            if (roles == null) return NotFound();
            return Ok(roles);
        }

        [HttpGet("roles/{roleId}")]
        public async Task<ActionResult<Game>> Get(Guid roleId)
        {
            var role = await roleService.GetRole(roleId);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet("players/{playerId}/role")]
        public async Task<ActionResult<Role>> GetPlayerRole(Guid playerId)
        {
            var role = await roleService.GetPlayerRole(playerId);
            if (role == null) return NotFound("Couldn't get player's role");
            return Ok(role);
        }

        [HttpDelete("roles/{roleId}")]
        public async Task<ActionResult> Delete(Guid roleId)
        {
            var role = await roleService.GetRole(roleId);
            if (role == null) return NotFound();

            await roleService.DeleteRole(roleId);
            return Ok("Role deleted");
        }
    }
}
