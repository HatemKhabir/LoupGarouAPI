using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRole(string request);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRole(string roleId);
        Task DeleteRole(string roleId);
    }
}
