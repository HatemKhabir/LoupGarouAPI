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

    //Salvs protected player1
    //Wolves voted on player2
    //Witch refused to revive
    //witch killed no one
    //? Player2 is dead
    //village voted on player1  
}
