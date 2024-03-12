using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRole(CreateRoleRequest request);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<IEnumerable<Role>> GetGameRoles(Guid gameId);
        Task<Role> GetRole(Guid roleId);
        Task DeleteRole(Guid roleId);
    }
}
