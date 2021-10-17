using AutoMapper;
using Core.Entities.Police;
using Core.Models.NewModels;

namespace TrafficControl.Core.AutoMapperProfile
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            #region TicketList
            CreateMap<TicketsList, TicketListDTO>();
            #endregion

            #region Tickets
            CreateMap<Ticket, TicketDTO>()
               .ForMember(dest =>
           dest.Car.CarName,
           opt => opt.MapFrom(src => src.Car.carsList.Name))

               .ForMember(dest =>
           dest.Car.OwnerName,
           opt => opt.MapFrom(src => src.Car.Owner.Name))

           .ForMember(dest =>
           dest.Car.PlateNumber,
           opt => opt.MapFrom(src => src.Car.PlateNumber))

           .ForMember(dest =>
           dest.TicketsList.Id,
           opt => opt.MapFrom(src => src.TicketsList.Id))

           .ForMember(dest =>
           dest.TicketsList.Name,
           opt => opt.MapFrom(src => src.TicketsList.Name))

           .ForMember(dest =>
           dest.TicketsList.Price,
           opt => opt.MapFrom(src => src.TicketsList.Price))
           .ReverseMap();
            #endregion

            #region Person
            CreateMap<Person, PersonDTO>()
            .ForMember(dest =>
            dest.Cars,
            opt => opt.MapFrom(src => src.Cars)).ReverseMap();
            #endregion
        }

    }
}
