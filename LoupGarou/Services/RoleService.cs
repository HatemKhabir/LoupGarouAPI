using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoupGarou.Services;
public class RoleService : IRoleService
{
    private readonly LoupGarouDbContext _loupGarouDbContext;

    public RoleService(LoupGarouDbContext loupGarouDbContext)
    {
        _loupGarouDbContext = loupGarouDbContext;
    }

    public async Task<Role> CreateRole(CreateRoleRequest request)
    {
        if (request == null || request.GameId == Guid.Empty || request.CardId == Guid.Empty) return null;

        var game = await GetGameById(request.GameId);
        if (game == null) return null;

        Role newRole = new Role()
        {
            RoleId = Guid.NewGuid(),
            Game = game,
            GameId = request.GameId,
            CardId = request.CardId
        };
        _loupGarouDbContext.Roles.Add(newRole);
        await _loupGarouDbContext.SaveChangesAsync();

        return newRole;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var allRoles = await _loupGarouDbContext.Roles.ToListAsync();
        return allRoles;
    }

    public async Task<Role> GetRole(Guid roleId)
    {
        var role = await _loupGarouDbContext.Roles.FindAsync(roleId);
        return role;
    }

    public async Task DeleteRole(Guid roleId)
    {
        var role = await _loupGarouDbContext.Roles.FindAsync(roleId);
        _loupGarouDbContext.Roles.Remove(role);
        await _loupGarouDbContext.SaveChangesAsync();
    }

    private async Task<Game> GetGameById(Guid gameId)
    {

        var game = await _loupGarouDbContext
          .Games
          .Include(g => g.Players)
          .Include(g => g.Roles)
          .FirstOrDefaultAsync(g => g.GameId == gameId);
        return game;
    }
}
