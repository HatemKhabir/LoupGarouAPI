using Azure.Core;
using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
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

    public Task<Role> CreateRole(CreateRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRole(string roleId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Role>> GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetRole(string roleId)
    {
        throw new NotImplementedException();
    }
}
