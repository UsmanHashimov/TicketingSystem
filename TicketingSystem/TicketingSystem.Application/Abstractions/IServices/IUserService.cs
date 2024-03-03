using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Abstractions.IServices
{
    public interface IUserService
    {
        public Task<string> Create(UserDTO userDTO);
        public Task<User> GetByName(string name);
        public Task<User> GetById(int Id);
        public Task<User> GetByEmail(string email);
        public Task<IEnumerable<User>> GetAll();
        public Task<string> Delete(int id);
        public Task<string> Update(int Id, UserDTO userDTO);
    }
}
