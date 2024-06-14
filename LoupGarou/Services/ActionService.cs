using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoupGarou.Services
{
	public class ActionService : IActionService
	{
		private readonly LoupGarouDbContext _context;
		public ActionService(LoupGarouDbContext context)
		{
			_context = context;
		}

		public async Task<Model.Action> CreateAction(CreateActionRequest request)
		{
			if (request==null) throw new ArgumentNullException(nameof(request));
			Model.Action activity = new Model.Action() {
				ActionId = new Guid(),
				TargetId = request.TargetId,
				PlayerId= request.PlayerId,
				ActionType= request.ActionType
			};
			_context.Actions.Add(activity);
			await _context.SaveChangesAsync();
			return activity;
		}

		public Task DeleteAction(Guid actionId)
		{
			throw new NotImplementedException();
		}

		public async Task<Model.Action?> GetAction(Guid actionId)
		{
			var action=await _context.Actions.FindAsync(actionId);
			return action;		  
		}

		public async Task<IEnumerable<Model.Action>> GetAllActions()
		{
			var action = await _context.Actions.ToListAsync();
			return action;
		}
	}
}
