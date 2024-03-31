namespace LoupGarou.Model.Responses
{
	public class ReturnPlayerResponse
	{
		public Guid PlayerId { get; set; }
		public string Name { get; set; }=string.Empty;
		public Guid RoleId { get; set; }
		public string Status { get; set; } = "alive";
		public Guid GameId { get; set; }
	}
}
