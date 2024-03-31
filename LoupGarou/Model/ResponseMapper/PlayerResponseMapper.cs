using LoupGarou.Model.Responses;

namespace LoupGarou.Model.ResponseMapper
{
	public static class PlayerResponseMapper
	{
		public static ReturnPlayerResponse ToPlayerResponse(this Player player)
		{
			return new ReturnPlayerResponse
			{
				PlayerId = player.PlayerId,
				Name = player.Name,
				RoleId = player.RoleId,
				Status = player.Status,
				GameId = player.GameId
			};
		}
	}
}
