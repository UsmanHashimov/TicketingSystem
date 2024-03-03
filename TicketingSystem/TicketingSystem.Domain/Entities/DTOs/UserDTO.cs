using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Domain.Entities.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Login { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string role { get; set; }
    }
}
