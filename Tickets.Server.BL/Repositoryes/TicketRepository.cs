using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Server.Contracts.Interfaces;
using Tickets.Server.Domain.Models;
using Tickets.Server.EntityFramework;

namespace Tickets.Server.BL.Repositoryes
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _db;

        public TicketRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddTicket(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _db.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetTicketById(Guid id)
        {
            return await _db.Tickets.FindAsync(id);
        }

        public async Task RemoveTicket(Guid id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _db.Tickets.Remove(ticket);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
            await _db.SaveChangesAsync();
        }
    }
}
