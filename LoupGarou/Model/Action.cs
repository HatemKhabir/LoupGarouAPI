namespace LoupGarou.Model
{
    public class Action
    {
        public Guid ActionId { get; set; }
        public Guid PlayerId{ get; set; }
        public Guid TargetId { get; set; }
        public string Name{ get; set; }
        public string ActionType { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
