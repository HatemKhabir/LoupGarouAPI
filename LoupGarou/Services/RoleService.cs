using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoupGarou.Services;
public class RoleService : IRoleService
{
    private readonly LoupGarouDbContext _loupGarouDbContext;
    private readonly ICardService cardService;

    public RoleService(LoupGarouDbContext loupGarouDbContext, ICardService cardService)
    {
        _loupGarouDbContext = loupGarouDbContext;
        this.cardService = cardService;
    }

    public async Task<Role> CreateRole(CreateRoleRequest request)
    {
        if (request == null || request.GameId == Guid.Empty || request.CardId == Guid.Empty) return null;

        var game = await GetGameById(request.GameId);
        if (game == null) return null;

        var card = await cardService.GetCard(request.CardId);
        if (card == null) return null;

        Role newRole = new Role()
        {
            RoleId = Guid.NewGuid(),
            Game = game,
            GameId = request.GameId,
            CardId = request.CardId,
            Card = card
        };

        _loupGarouDbContext.Roles.Add(newRole);
        await _loupGarouDbContext.SaveChangesAsync();

        return newRole;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var allRoles = await _loupGarouDbContext.Roles
            .Include(r => r.Card)
            .ToListAsync();
        return allRoles;
    }

    public async Task<IEnumerable<Role>> GetGameRoles(Guid gameId)
    {
        var game = await GetGameById(gameId);
        if (game == null) return null;
        return game.Roles;
    }

    public async Task<Role> GetRole(Guid roleId)
    {
        var role = await _loupGarouDbContext.Roles.Include(r => r.Card).FirstOrDefaultAsync(r => r.RoleId == roleId);
        return role;
    }

    public async Task<Role> GetPlayerRole(Guid playerId)
    {
        if (playerId == Guid.Empty) return null;

        Player player = await GetPlayer(playerId);
        if (player == null) return null;

        var roleId = player.RoleId;
        Role role = await GetRole(roleId);
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
          .ThenInclude(r => r.Card)
          .FirstOrDefaultAsync(g => g.GameId == gameId);
        return game;
    }
    private async Task<Player> GetPlayer(Guid playerId)
    {
        var player = await _loupGarouDbContext.Players.FindAsync(playerId);
        return player;
    }
}
