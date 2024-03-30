
using Tickets.Server.Contracts.Ticket;
using Tickets.Server.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Tickets.Server.Contracts.Interfaces;

namespace Tickets.Server.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // POST api/ticket
        [HttpPost]
        public async Task<IActionResult> Post(CreateUpdateTicketDto ticketDto)
        {
            if (ticketDto == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                await _ticketService.CreateTicketAsync(ticketDto);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
           
            return Ok();
        }

        // PUT api/ticket/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateUpdateTicketDto ticketDto)
        {
            if (id != ticketDto.Id)
            {
                return BadRequest();
            }

            try
            {
                bool result = await _ticketService.UpdateTicketAsync(ticketDto);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("Нельзя редактировать отправленную заявку");
                }
            }
            catch (ArgumentException ex)
            {
                return Content(ex.Message);
            }
        }
        // DELETE api/ticket/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Логика для удаления заявки
            try
            {
                var res = await _ticketService.DeleteTicketAsync(id);


                if (res)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("Ошибка при удалении заявки");
                }
            }
            catch (ArgumentException ex)
            {
                return Content(ex.Message);
            }

        }

        // POST api/ticket/submit/{id}
        [HttpPost("submit/{id}")]
        public async Task<IActionResult> SubmitForReview(Guid id)
        {
            // Логика для отправки заявки на рассмотрение
            var res = await _ticketService.SendTicketForReviewAsync(id);
            if (res)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/ticket/createdAfter/{date}
        [HttpGet("createdAfter/{date}")]
        public async Task<IEnumerable<TicketDto>> GetTicketsCreatedAfter(DateTime date)
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var filteredTickets = tickets.Where(x => x.CreateDate > date).ToList();
            return filteredTickets;
        }
        // GET api/ticket/notSubmittedAndOlderThan/{date}
        [HttpGet("notSubmittedAndOlderThan/{date}")]
        public async Task<IEnumerable<TicketDto>> GetNotSubmittedTicketsOlderThan(DateTime date)
        {
            // Логика для получения заявок не поданных и старше определенной даты
            var tickets = await _ticketService.GetAllTicketsAsync();

            return tickets
                .Where(x => !x.IsUnderReview && x.CreateDate < date);
        }
        // GET api/ticket/user/{userId}/current
        [HttpGet("user/{userId}/current")]
        public async Task<TicketDto> GetCurrentTicketDto(Guid userId)
        {
            // Логика для получения текущей не поданной заявки для указанного пользователя
            var currentTicket = (await _ticketService.GetAllTicketsAsync()).Where(x => x.UserId == userId && !x.IsUnderReview).FirstOrDefault();

            return currentTicket ?? new TicketDto();
        }

        // GET api/ticket/{id}
        [HttpGet("{id}")]
        public async Task<TicketDto> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            return ticket;
        }

        // GET api/ticket/activityTypes
        [HttpGet("activityTypes")]
        public IEnumerable<ActivityTypeDto> GetActivityTypes()
        {
            var activityTypes = Enum.GetValues(typeof(ActivityType))
                                    .Cast<ActivityType>()
                                    .Select(at => new ActivityTypeDto
                                    {
                                        Activity = at.ToString(),
                                        Description = GetDescription(at)
                                    });

            return activityTypes;
        }

        private string GetDescription(ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.Presentation:
                    return "Доклад, 35-45 минут";
                case ActivityType.Workshop:
                    return "Мастеркласс, 1-2 часа";
                case ActivityType.Discussion:
                    return "Дискуссия / круглый стол, 40-50 минут";
                default:
                    return string.Empty;
            }
        }
    }
}
