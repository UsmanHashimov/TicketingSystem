using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<ResponceLogin> GenerateToken(RequestLogin request);
    }
}
