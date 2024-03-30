using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Server.Contracts.Ticket;

namespace Tickets.Server.Contracts.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicketAsync(CreateUpdateTicketDto ticket);
        Task<bool> SendTicketForReviewAsync(Guid id);
        Task<List<TicketDto>> GetAllTicketsAsync();
        Task<TicketDto> GetTicketByIdAsync(Guid id);
        Task<bool> UpdateTicketAsync(CreateUpdateTicketDto ticket);
        Task<bool> DeleteTicketAsync(Guid id);
    }
}
