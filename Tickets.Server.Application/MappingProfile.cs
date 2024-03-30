using AutoMapper;
using Tickets.Server.Contracts.Ticket;
using Tickets.Server.Domain.Models;

namespace Tickets.Server.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TicketDto, Ticket>()
       .ForMember(dest => dest.ActivityType,
                  opt => opt.MapFrom(src => Enum.Parse<ActivityType>(src.ActivityType.ToString())));

            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.ActivityType,
                           opt => opt.MapFrom(src => Enum.GetName(typeof(ActivityType), src.ActivityType)));

            CreateMap<CreateUpdateTicketDto, Ticket>();

        }
    }
}
