using LoupGarou.Model;
using LoupGarou.Model.Requests;
using Action = LoupGarou.Model.Action;

namespace LoupGarou.Services.Interfaces
{
    public interface IActionService
    {
        Task<Action> CreateAction(CreateActionRequest request);
        Task<IEnumerable<Action>> GetAllActions();
        Task<Action> GetAction(Guid ationId); 
        Task DeleteAction(Guid actionId);
    }
}
