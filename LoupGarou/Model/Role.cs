using System.Text.Json.Serialization;

namespace LoupGarou.Model
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public Guid CardId { get; set; }
        public Guid GameId { get; set; }
        [JsonIgnore]
        public Game Game{ get; set; }
    }
}
