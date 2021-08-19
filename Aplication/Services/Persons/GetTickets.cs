using Aplication.Services.Persons.Dtos;
using Aplication.Services.Police.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Persons
{
    public interface IGetTickets
    {
        public List<GetAllCarsDTO> GetAllTickets(string Name);

    }

    public class GetTickets : IGetTickets
    {
        private readonly IDatabasecontextPolice _context;
        public GetTickets(IDatabasecontextPolice context)
        {
            _context = context;
        }


        public List<GetAllCarsDTO> GetAllTickets(string Name)
        {

            List<GetAllCarsDTO> CarslistsList = new List<GetAllCarsDTO>();
            var AllPersonsCars = _context.persons.Include(p=>p.Cars).ThenInclude(p=>p.carsList).Include(p => p.Cars).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketsList).FirstOrDefault(p => p.Name == Name);
            var PersonCar = AllPersonsCars.Cars;

                List<GetAllCarsDTO> PersonsCarsList = new List<GetAllCarsDTO>();
                foreach (var Car in PersonCar)
                {
                    List<GetAllCarsTicketsDTO> getAllCarsTicketsDTOs = new List<GetAllCarsTicketsDTO>();
                    foreach (var Tickets in Car.Tickets)
                    {
                        getAllCarsTicketsDTOs.Add(new GetAllCarsTicketsDTO()
                        {
                            Name = Tickets.Car.Owner.Name,
                            Price = Tickets.TicketsList.Price,
                            TicketDate = Tickets.TicketDate,
                            TicketId = Tickets.Id
                        });
                    }

                    PersonsCarsList.Add(new GetAllCarsDTO()
                    {
                        CarId = Car.Id,
                        Owner = Car.Owner.Name,
                        PlateNumber = Car.PlateNumber,
                        Tickets = getAllCarsTicketsDTOs,
                        CarName = Car.carsList.Name
                    });
                }
                
            return PersonsCarsList;

        }
    }
}
