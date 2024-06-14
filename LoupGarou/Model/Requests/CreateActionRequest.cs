using LoupGarou.Utils;

namespace LoupGarou.Model.Requests
{
	public class CreateActionRequest
	{
		public Guid ActionId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid TargetId { get; set; }
		public Actions ActionType { get; set; }//0,1,2 {Kill,Protect,Revive}
	}
}
