using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoupGarou.Services;
public class RoleService : IRoleService
{
  private readonly LoupGarouDbContext loupGarouDbContext;

  public RoleService(LoupGarouDbContext loupGarouDbContext)
  {
    this.loupGarouDbContext = loupGarouDbContext;
  }

    public async Task<Role> CreateRole(CreateRoleRequest request)
    {
        if (request == null || request.RoleName.IsNullOrEmpty()) return null;

        Role newRole = new Role()
        {
            RoleId = new Guid(),
            RoleName= request.RoleName,
            Description= request.Description,
            Ability= request.Ability,
        };
        loupGarouDbContext.Roles.Add(newRole);
        await loupGarouDbContext.SaveChangesAsync();

        return newRole;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var allRoles= await loupGarouDbContext.Roles.ToListAsync();
        return allRoles;
    }

    public async Task<Role> GetRole(Guid roleId)
    {
        var role = await loupGarouDbContext.Roles.FindAsync(roleId);
        return role;
    }

    public async Task DeleteRole(Guid roleId)
    {
        var role = await loupGarouDbContext.Roles.FindAsync(roleId);
        loupGarouDbContext.Roles.Remove(role);
        await loupGarouDbContext.SaveChangesAsync();
    }
}
