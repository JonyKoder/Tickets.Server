using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Server.Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Plan { get; set; }
        public bool IsUnderReview { get; set; }

        // Методы доступа для установки и получения значений свойств
        public void SetId(Guid id)
        {
            Id = id;
        }

        public Guid GetId()
        {
            return Id;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public Guid GetUserId()
        {
            return UserId;
        }

        public void SetIsUnderReview(bool isUnderReview)
        {
            IsUnderReview = isUnderReview;
        }

        public bool GetIsUnderReview()
        {
            return IsUnderReview;
        }
    }
  
}
