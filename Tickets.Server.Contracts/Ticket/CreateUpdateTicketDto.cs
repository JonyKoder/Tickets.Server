using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Server.Domain.Models;

namespace Tickets.Server.Contracts.Ticket
{
    public class CreateUpdateTicketDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }

        public ActivityType ActivityType { get; set; } = 0;

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название должно быть не более 100 символов")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "Краткое описание должно быть не более 300 символов")]
        [Required(ErrorMessage = "Описание обязательное для заполнения")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "План обязателен")]
        [StringLength(1000, ErrorMessage = "План должен быть не более 1000 символов")]
        public string Plan { get; set; }
    }
}
