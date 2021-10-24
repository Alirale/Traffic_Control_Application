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


            #region CarToRegisterCarDTO
            CreateMap<Car, RegisterCarDTO>()
            .ForMember(dest => dest.PlateNumber,
            opt => opt.MapFrom(src => src.PlateNumber))

            .ForMember(dest => dest.Owner,
            opt => opt.MapFrom(src => src.Owner))

            .ForMember(dest => dest.CarsList,
            opt => opt.MapFrom(src => src.carsList)).ReverseMap();
            #endregion

            #region PersonToOwnerDTO
            CreateMap<Person, OwnerDTO>()
                .ForMember(dest => dest.OwnerName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();
            #endregion


            #region CarToRegisterCarREsponseDTO
            CreateMap<Car, RegisterCarREsponseDTO>()
            .ForMember(dest => dest.CarId,
            opt => opt.MapFrom(src => src.Id))

            .ForMember(dest => dest.PlateNumber,
            opt => opt.MapFrom(src => src.PlateNumber))

            .ForMember(dest => dest.Owner,
            opt => opt.MapFrom(src => src.Owner))

            .ForMember(dest => dest.CarsList,
            opt => opt.MapFrom(src => src.carsList)).ReverseMap();
            #endregion


            #region PersonToOwnerDTO
            CreateMap<Person, OwnerDTO>()
                .ForMember(dest => dest.OwnerName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();
            #endregion


            #region CarsListToCarslistDTO
            CreateMap<CarsList, CarslistDTO>()
             .ForMember(dest => dest.CarName,
             opt => opt.MapFrom(src => src.Name)).ReverseMap();
            #endregion
        }

    }
}
