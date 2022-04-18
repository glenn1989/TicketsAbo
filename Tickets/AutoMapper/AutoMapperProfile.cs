using AutoMapper;
using Tickets.Domain.Entities;
using Tickets.ViewModel;

namespace Tickets.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Club, ClubVM>()
                .ForMember(dest => dest.Stadion,
                opts => opts
                .MapFrom(src => src.Stadion.StadionNaam))
                .ForMember(dest => dest.Capaciteit, opts => opts
                .MapFrom(src => src.Stadion.StadionCapaciteit));

            CreateMap<Wedstrijd, WedstrijdVM>()
                .ForMember(dest => dest.Thuisploeg,
                opts => opts
                .MapFrom(src => src.Thuisploeg.Clubnaam))
                .ForMember(dest => dest.Uitploeg, opts => opts
                .MapFrom(src => src.Uitploeg.Clubnaam));

            CreateMap<Wedstrijd, WedstrijdlistVM>()
                .ForMember(dest => dest.Thuisploeg,
                opts => opts
                .MapFrom(src => src.Thuisploeg.Clubnaam))
                .ForMember(dest => dest.Uitploeg,
                opts => opts
                .MapFrom(src => src.Uitploeg.Clubnaam))
                .ForMember(dest => dest.ThuisploegId,
                opts => opts
                .MapFrom(src => src.ThuisploegId))
                .ForMember(dest => dest.UitploegId,
                opts => opts
                .MapFrom(src => src.UitploegId))
                .ForMember(dest => dest.Datum,
                opts => opts
                .MapFrom(src => src.Datum))
                .ForMember(dest => dest.Uur,
                opts => opts
                .MapFrom(src => src.Uur))
                .ForMember(dest => dest.WedstrijdId,
                opts => opts
                .MapFrom(src => src.WedstrijdId));


            CreateMap<Ticket, TicketVM>()
                .ForMember(dest => dest.Thuisploeg, opts => opts
                .MapFrom(src => src.Wedstrijd.Thuisploeg.Clubnaam))
                .ForMember(dest => dest.Thuisploeg, opts => opts
                .MapFrom(src => src.Wedstrijd.Uitploeg.Clubnaam))
                .ForMember(dest => dest.Vak, opts => opts
                .MapFrom(src => src.Plaats.VakStadion.Vak.VakNaam))
                .ForMember(dest => dest.Prijs, opts => opts
                .MapFrom(src => src.Plaats.VakStadion.Prijs));

            CreateMap<OrderVM, Aankopen>()
                .ForMember(dest => dest.ClientId, opts => opts
                .MapFrom(src => src.UserID));

            
        }
    }
}
