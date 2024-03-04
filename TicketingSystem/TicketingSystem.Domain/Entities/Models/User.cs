using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Domain.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [EmailAddress]
        public string Login { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string role { get; set; }
        public int TicketId { get; set; }
    }
}
