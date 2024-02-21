using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoupGarou.Controllers
{
    [Route("TODO/api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService service)
        {
            roleService = service;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateRoleRequest request)
        {
            if (request == null || request.RoleName.IsNullOrEmpty()) return BadRequest($"Please send a valid request");
            Role role = await roleService.CreateRole(request);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var getUrl = baseUrl + "/api/roles/" + role.RoleId;
            return Ok("This should create a new role (Loup/ witch/ hunter .. ) and returna unique ID");
            //return Created(getUrl, role);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            var allroles = await roleService.GetAllRoles();
            if (allroles == null) return NoContent();
            return Ok("This should return all roles in the DB");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<ActionResult<Game>> Get(string roleId)
        {
            var role = await roleService.GetRole(roleId);
            if (role == null) return NotFound();
            return Ok("This should return the role matching a specified ID (Loup/Salvaddor..)");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        public async Task<ActionResult> Delete(string roleId)
        {
            var role = await roleService.GetRole(roleId);
            if (role == null) return NotFound();

            await roleService.DeleteRole(roleId);
            return Ok("This should delete a role matching the specified ID");
        }
    }
}
