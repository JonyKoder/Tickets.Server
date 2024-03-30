using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Server.Contracts.Interfaces;
using Tickets.Server.Contracts.Ticket;
using Tickets.Server.Domain.Models;

namespace Tickets.Server.BL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task CreateTicketAsync(CreateUpdateTicketDto ticket)
        {

            var mappedTicket = _mapper.Map<CreateUpdateTicketDto, Ticket>(ticket);
            mappedTicket.SetIsUnderReview(false);
            mappedTicket.CreateDate = DateTime.Now.ToUniversalTime();
            var userExistsDraft = (await _ticketRepository.GetAllTickets()).Any(x => !x.IsUnderReview);
            if (userExistsDraft)
            {
                throw new Exception("У пользователя может быть только одна не отправленная заявка");
            }
            await _ticketRepository.AddTicket(mappedTicket);


        }

        public async Task<List<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _ticketRepository.GetAllTickets();
            return tickets.Select(t => _mapper.Map<Ticket, TicketDto>(t)).ToList();
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetTicketById(id);
            return _mapper.Map<Ticket, TicketDto>(ticket);
        }

        public async Task<bool> UpdateTicketAsync(CreateUpdateTicketDto ticket)
        {
            var existingTicket = await _ticketRepository.GetTicketById(ticket.Id);

            if (existingTicket == null)
            {
                throw new ArgumentException("Заявка не найдена", nameof(ticket.Id));
            }

            if (existingTicket.IsUnderReview)
            {
                return false;
            }

            await _ticketRepository.UpdateTicket(_mapper.Map<CreateUpdateTicketDto, Ticket>(ticket));
            return true;
        }

        public async Task<bool> DeleteTicketAsync(Guid id)
        {
            var existingTicket = await _ticketRepository.GetTicketById(id);
            if (existingTicket.IsUnderReview)
            {
                throw new ArgumentException("Нельзя удалить заявки отправленные на рассмотрение");
            }
            if (existingTicket == null)
            {
                return false;
            }

            await _ticketRepository.RemoveTicket(id);
            return true;
        }

        public async Task<bool> SendTicketForReviewAsync(Guid id)
        {
            var existingTicket = await _ticketRepository.GetTicketById(id);

            if (existingTicket == null)
            {
                return false;
            }

            existingTicket.SetIsUnderReview(true);

            await _ticketRepository.UpdateTicket(existingTicket);

            return true;
        }
    }

}
