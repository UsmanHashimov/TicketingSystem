using TicketingSystem.Application.Abstractions.IRepositories;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
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
            var result = await _ticketRepository.Delete(x => x.Id == id);

            return result;
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
            return result;
        }

        public async Task<string> Update(int Id, TicketDTO ticketDTO)
        {
            var res = await _ticketRepository.GetByAny(x => x.Id == Id);

            if (res != null)
            {
                var Ticket = new Ticket()
                {
                    TicketName = ticketDTO.TicketName,
                    TicketDescription = ticketDTO.TicketDescription
                };
                var result = await _ticketRepository.Update(Ticket);

                return "Updated";
            }
            return "Failed";

        }
    }
}
