using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Server.Domain.Models;

namespace Tickets.Server.Contracts.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Tickets.Server.Domain.Models.Ticket>> GetAllTickets();

        Task<Tickets.Server.Domain.Models.Ticket> GetTicketById(Guid id);

        Task AddTicket(Tickets.Server.Domain.Models.Ticket ticket);

        Task UpdateTicket(Tickets.Server.Domain.Models.Ticket ticket);

        Task RemoveTicket(Guid id);
    }
}
