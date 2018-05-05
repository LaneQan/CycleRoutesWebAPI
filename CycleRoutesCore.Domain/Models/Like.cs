namespace CycleRoutesCore.Domain.Models
{
    public class Like
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public User User { get; set; }
        public bool State { get; set; }
    }
}