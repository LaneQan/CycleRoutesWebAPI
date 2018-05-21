namespace CycleRoutesCore.Domain.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }
    }
}