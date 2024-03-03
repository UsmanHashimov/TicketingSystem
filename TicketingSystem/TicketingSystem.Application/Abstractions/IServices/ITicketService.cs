﻿using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Abstractions.IServices
{
    public interface ITicketService
    {
        public Task<string> Create(TicketDTO TicketDTO);
        public Task<Ticket> GetByName(string name);
        public Task<IEnumerable<Ticket>> GetAll();
        public Task<bool> Delete(int id);
        public Task<string> Update(int Id, TicketDTO TicketDTO);
    }
}
