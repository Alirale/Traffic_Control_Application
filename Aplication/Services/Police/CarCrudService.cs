using Aplication.Services.Police.Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Police
{
    public interface ICarCrudService
    {
        public List<List<GetAllCarsDTO>> GetAll();
        public GetCarTicketDTO Get(int Id);
        public object Add(AddCarTicket NewCarTicket);
        public void Delete(int Id);
        public bool Edit(EditCarTicket edit);
    }


    public class CarCrudService : ICarCrudService
    {
        private readonly IDatabasecontextPolice _context;


        public CarCrudService(IDatabasecontextPolice context)
        {
            _context = context;
        }

        public List<List<GetAllCarsDTO>> GetAll()
        {
            List<List<GetAllCarsDTO>> CarslistsList = new List<List<GetAllCarsDTO>>();
            var AllPersonsCars = _context.persons.Include(p => p.Cars).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketsList).Include(p =>p.Cars).ThenInclude(p=>p.carsList).ToList();

            foreach (var Person in AllPersonsCars)
            {
                List<GetAllCarsDTO> PersonsCarsList = new List<GetAllCarsDTO>();
                foreach (var Car in Person.Cars)
                {
                    List<GetAllCarsTicketsDTO> getAllCarsTicketsDTOs = new List<GetAllCarsTicketsDTO>();
                    foreach (var Tickets in Car.Tickets)
                    {
                        getAllCarsTicketsDTOs.Add(new GetAllCarsTicketsDTO()
                        {
                            Name = Tickets.TicketsList.Name,
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
                CarslistsList.Add(PersonsCarsList);
            }


            return CarslistsList;
        }

        public GetCarTicketDTO Get(int Id)
        {
            var CarsList = _context.cars.Include(p => p.carsList).Include(p => p.Tickets).FirstOrDefault(p => p.Id == Id);
            return new GetCarTicketDTO()
            {
                CarId = CarsList.Id,
                CarName = CarsList.carsList.Name,
                PlateNumber = CarsList.PlateNumber,
                Tickets = CarsList.Tickets
            };
        }

        public object Add(AddCarTicket NewCarTicket)
        {
            var PersonOwnedThatCar = _context.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).Include(p => p.Cars).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketsList).FirstOrDefault(p => p.Cars.Any(x => x.PlateNumber == NewCarTicket.PlateNumber));
            if (PersonOwnedThatCar != null)
            {
                var HisCar = PersonOwnedThatCar.Cars.FirstOrDefault(p => p.PlateNumber == NewCarTicket.PlateNumber);
                var HisTickets = HisCar.Tickets;
                HisTickets.Add(new Ticket()
                {
                    Car = HisCar,
                    CarId = HisCar.Id,
                    TicketDate = DateTime.Now,
                    TicketsList = _context.ticketsList.First(p => p.Id == NewCarTicket.TicketsListId)
                });
            }
            else
            {
                _context.tickets.Add(new Ticket()
                {
                    Car = _context.cars.First(p => p.PlateNumber == NewCarTicket.PlateNumber),
                    TicketDate = DateTime.Now,
                    TicketsList = _context.ticketsList.First(p => p.Id == NewCarTicket.TicketsListId)
                });
            }
            _context.SaveChanges();
            return "Done";
        }



        public void Delete(int Id)
        {
            _context.tickets.Remove(new Ticket { Id = Id });
            _context.SaveChanges();
        }

        public bool Edit(EditCarTicket edit)
        {
            var tickets = _context.tickets.Include(p => p.Car).Include(p => p.TicketsList).FirstOrDefault(p => p.Id == edit.Id);
            var Edited_TicketList = _context.ticketsList.First(p => p.Id == edit.TicketsListId);
            tickets.TicketsList = Edited_TicketList;
            tickets.Car.PlateNumber = edit.PlateNumber;
            _context.SaveChanges();
            return true;
        }
    }
}
