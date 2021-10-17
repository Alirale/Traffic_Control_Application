using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models.NewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrafficControl.Aplication.Services.Person
{
    public interface IGetTickets
    {
        public Task<PersonDTO> GetAllTickets(string Name);

    }

    public class GetTickets : IGetTickets
    {
        private readonly IPersonRepository _PersonRepository;
        public GetTickets(IPersonRepository PersonRepository)
        {
            _PersonRepository = PersonRepository;
        }


        public async Task<PersonDTO> GetAllTickets(string Name)
        {
            var Person = await _PersonRepository.GetPersonByName(Name);
            var CarDTOList = new List<CarDTO>();
            var Output = new PersonDTO();
            foreach (var Car in Person.Cars)
            {
                var TicketsDTOList = new List<TicketsDTO>();
                var CarTicket = Car.Tickets;
                foreach (var Ticket in CarTicket)
                {
                    TicketsDTOList.Add(new TicketsDTO() { TicketName = Ticket.TicketsList.Name, TicketPrice = Ticket.TicketsList.Price });
                }
                CarDTOList.Add(new CarDTO() { CarName = Car.carsList.Name, PlateNumber = Car.PlateNumber, TicketsList = TicketsDTOList });
            }
            Output.Cars = CarDTOList;
            return Output;
        }
    }
}
