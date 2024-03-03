namespace TicketingSystem.Domain.Entities.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? TicketName { get; set; }
        public string? TicketDescription { get; set; }
        public int Organisator { get; set; }
    }
}
