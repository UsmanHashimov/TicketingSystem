﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TicketingSystem.API.Attributes;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Enums;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [IdentityFilter(Permission.AddTicket)]
        public async Task<string> AddTicket(TicketDTO ticketDTO)
        {
            var res = await _ticketService.Create(ticketDTO);
            return res;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetAllTickets)]
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            return await _ticketService.GetAll();
        }

        //[HttpGet]
        //[IdentityFilter(Permission.GetByTicketId)]
        //public async Task<Ticket> GetTicketById(int id)
        //{
        //    var res = await _ticketService.
        //}
        [HttpGet]
        [IdentityFilter(Permission.GetByTicketName)]
        public async Task<Ticket> GetByTicketName(string name)
        {
            var res = await _ticketService.GetByName(name);
            return res;
        }

        [HttpDelete]
        [IdentityFilter(Permission.DeleteTicket)]
        public async Task<bool> DeleteTicket(int id)
        {
            var res = await _ticketService.Delete(id);

            return res;
        }

        [HttpPut]
        [IdentityFilter(Permission.UpdateTicket)]
        public async Task<string> UpdateTicket(int id, TicketDTO ticketDTO)
        {
            var res = await _ticketService.Update(id, ticketDTO);

            return res;
        }
    }
}
