using AutoMapper;
using Core.Entities.Police;
using Core.Models;

namespace TrafficControl.Core.AutoMapperProfile
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            #region TicketList
            CreateMap<TicketsList, TicketListDTO>().ReverseMap();
            #endregion


            #region Tickets
            CreateMap<Ticket, TicketDTO>()
            .ForMember(dest => dest.Car,
            opt => opt.MapFrom(src => src.Car))

            .ForMember(dest => dest.TicketsList,
            opt => opt.MapFrom(src => src.TicketsList)).ReverseMap();
            #endregion

            #region Person
            CreateMap<Person, PersonDTO>()
            .ForMember(dest =>
            dest.Cars,
            opt => opt.MapFrom(src => src.Cars)).ReverseMap();
            #endregion


            #region CarToCarDTO
            CreateMap<Car, CarTicketDTO>()
            .ForMember(dest => dest.CarName,
            opt => opt.MapFrom(src => src.carsList.Name))

            .ForMember(dest => dest.OwnerName,
            opt => opt.MapFrom(src => src.Owner.Name))

            .ForMember(dest => dest.PlateNumber,
            opt => opt.MapFrom(src => src.PlateNumber)).ReverseMap();
            #endregion
        }

    }
}
