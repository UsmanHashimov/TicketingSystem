using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Domain.Entities.Models
{
    public class RequestSignUp
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [EmailAddress]
        public string Login { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string role { get; set; }
    }
}
