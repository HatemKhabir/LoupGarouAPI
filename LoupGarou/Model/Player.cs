namespace LoupGarou.Model
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public bool IsProtected { get; set; } = false;
        public bool IsLover { get; set; } = false;
        public string Status { get; set; } = "alive";
        public string GameId { get; set; }
    }
}
