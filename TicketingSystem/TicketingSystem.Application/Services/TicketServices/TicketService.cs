using TicketingSystem.Application.Abstractions.IRepositories;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Exceptions;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }
        public async Task<string> PurchaseTicket(string email, string password, int id)
        {
            var userByEmail = await _userRepository.GetByAny(x => x.Email == email);
            var res = await _ticketRepository.GetByAny(x => x.Id == id);
            if (userByEmail != null)
            {
                if (userByEmail.Password == password)
                {
                    if (res != null)
                    {
                        userByEmail.TicketId = id;

                        await _userRepository.Update(userByEmail);
                        return "Purchased";
                    }
                    return "Such ticket not found";
                }
                return "Incorrect password";
            }
            return "Incorrect email";
        }

        public async Task<string> Create(TicketDTO ticketDTO)
        {
            var Ticket = new Ticket()
            {
                TicketName = ticketDTO.TicketName,
                TicketDescription = ticketDTO.TicketDescription
            };
            var result = await _ticketRepository.Create(Ticket);

            return "Created";
        }

        public async Task<bool> Delete(int id)
        {
            var ticketById = await _ticketRepository.GetByAny(x => x.Id == id);
            if (ticketById != null)
            {
                var usersWithTicket = await _userRepository.GetByAnyList(user => user.TicketId == id);

                foreach (var user in usersWithTicket)
                {
                    user.TicketId = 0;
                    await _userRepository.Update(user);
                }

                var result = await _ticketRepository.Delete(x => x.Id == id);

                return result;
            }
            throw new TicketNotFoundException("Ticket not found!");
        }


        public async Task<IEnumerable<Ticket>> GetAll()
        {
            var Tickets = await _ticketRepository.GetAll();

            var result = Tickets.Select(model => new Ticket
            {
                TicketName = model.TicketName,
                TicketDescription = model.TicketDescription
            });

            return result;
        }

        public async Task<Ticket> GetByName(string name)
        {
            var result = await _ticketRepository.GetByAny(d => d.TicketName == name);
            if (result != null)
            {
                return result;
            }
            throw new TicketNotFoundException("Ticket not found!");
        }

        public async Task<Ticket> GetById(int id)
        {
            var result = await _ticketRepository.GetByAny(d => d.Id == id);
            if (result != null)
            {
                return result;
            }
            throw new TicketNotFoundException("Ticket not found!");
        }

        public async Task<string> Update(int Id, TicketDTO ticketDTO)
        {
            var res = await _ticketRepository.GetByAny(x => x.Id == Id);
            var name = await _ticketRepository.GetByAny(x => x.TicketName == ticketDTO.TicketName);

            if (res != null)
            {
                if (name == null)
                {
                    res.TicketName = ticketDTO.TicketName;
                    res.TicketDescription = ticketDTO.TicketDescription;

                    await _ticketRepository.Update(res);

                    return "Updated";
                }
                return "Such ticket already exists";
            }
            return "Such ticket not found";

        }
    }
}
