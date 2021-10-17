using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Core.Models.NewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficControl.Aplication.Services.Police
{
    public interface ICarCrudService
    {
        public Task<List<TicketDTO>> GetAll();
        public Task<TicketDTO> Get(int Id);
        public Task<bool> Add(AddCarTicketDTO NewCarTicket);
        public Task<bool> Delete(int Id);
        public Task<bool> Edit(EditCarTicketDTO edit);
    }


    public class CarCrudService : ICarCrudService
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly ITicketListRepository _TicketListRepository;
        private readonly ICarRepository _CarRepository;


        public CarCrudService(ITicketRepository TicketRepository, ITicketListRepository TicketListRepository, ICarRepository CarRepository)
        {
            _TicketRepository = TicketRepository;
            _TicketListRepository = TicketListRepository;
            _CarRepository = CarRepository;
        }

        public async Task<List<TicketDTO>> GetAll()
        {
            return await _TicketRepository.GetAllTickets();
        }
        
        public async Task<TicketDTO> Get(int Id)
        {
           return await _TicketRepository.GetTicketDTOById(Id);
        }

        public async Task<bool> Add(AddCarTicketDTO NewCarTicket)
        {
            try
            {
                var TicketList =await _TicketListRepository.GetTicketListById(NewCarTicket.TicketsListId);
                var Car = await _CarRepository.GetCarByPlateNumber(NewCarTicket.PlateNumber);
                Car.Tickets.Add(new Ticket() {Car = Car,CarId = Car.Id,TicketDate = DateTime.Now ,TicketsList = TicketList });
                var result = await _TicketRepository.AddTicket(Car);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Edit(EditCarTicketDTO edit)
        {
            try
            {
                var ThatTicket = await _TicketRepository.GetTicketById(edit.TicketId);
                var TicketList = await _TicketListRepository.GetTicketListById(edit.TicketsListId);
                var Car = await _CarRepository.GetCarByPlateNumber(edit.PlateNumber);
                ThatTicket.Car = Car;ThatTicket.CarId = Car.Id;
                ThatTicket.TicketsList = TicketList;ThatTicket.TicketDate = DateTime.Now;
                var result = await _TicketRepository.EditTicket(ThatTicket);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var response =await _TicketRepository.DeleteTicket(Id);
                return response;
            }
            catch
            {
                return false;
            }
        }

    }
}
