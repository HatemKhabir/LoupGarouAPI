using LoupGarou.Model;
using Action = LoupGarou.Model.Action;

namespace LoupGarou.Services.Interfaces
{
    public interface IActionService
    {
        Task<Action> CreateAction(string request);
        Task<IEnumerable<Action>> GetAllActions();
        Task<Action> GetAction(Guid ationId);
        Task DeleteAction(Guid actionId);
    }
}
