using Tickets.Server.Domain.Models;

namespace Tickets.Server.Contracts.Ticket
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ActivityType { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Plan { get; set; }
        public bool IsUnderReview { get; set; }
    }
}
