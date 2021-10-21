using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Police
{
    public interface ITicketService
    {
        public Task<List<TicketDTO>> GetAll();
        public Task<TicketDTO> Get(int Id);
        public Task<TicketModifyResponse> Add(AddCarTicketDTO NewCarTicket);
        public Task<bool> Delete(int Id);
        public Task<TicketModifyResponse> Edit(EditCarTicketDTO edit);
    }


    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly ITicketListRepository _TicketListRepository;
        private readonly ICarRepository _CarRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;


        public TicketService(ITicketRepository TicketRepository, ITicketListRepository TicketListRepository, ICarRepository CarRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _TicketRepository = TicketRepository;
            _TicketListRepository = TicketListRepository;
            _CarRepository = CarRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<List<TicketDTO>> GetAll()
        {
            List<TicketDTO> Output = new List<TicketDTO>();
            var Tickets = await _TicketRepository.GetAllTickets();
            Tickets.ForEach(ticket => {
                var Ticket = _mapper.Map<TicketDTO>(ticket);
                Output.Add(Ticket);
            });
            return Output;
        }

        public async Task<TicketDTO> Get(int Id)
        {
            return _mapper.Map<TicketDTO>(await _TicketRepository.GetTicketDTOById(Id));
        }

        public async Task<TicketModifyResponse> Add(AddCarTicketDTO NewCarTicket)
        {
            try
            {
                var TicketList = await _TicketListRepository.GetTicketListById(NewCarTicket.TicketsListId);
                var Car = await _CarRepository.GetCarByPlateNumber(NewCarTicket.PlateNumber);
                var tempTicket = new Ticket() { Car = Car, CarId = Car.Id, TicketDate = DateTime.Now, TicketsList = TicketList };
                Car.Tickets.Add(tempTicket);
                var result =await _TicketRepository.AddTicket(Car);
                var OutputTicket =await _TicketRepository.GetTicketDTOByKeyLessInfo(new IdLessTicket() { CarId = tempTicket.CarId, TicketDate = tempTicket.TicketDate, TicketsList = tempTicket.TicketsList });

                if (result)
                {
                    return new TicketModifyResponse() 
                    {
                       TicketId = OutputTicket.Id,PlateNumber =Car.PlateNumber,TicketsListId= OutputTicket.TicketsList.Id
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<TicketModifyResponse> Edit(EditCarTicketDTO edit)
        {
            try
            {
                var ThatTicket = await _TicketRepository.GetTicketById(edit.TicketId);
                var TicketList = await _TicketListRepository.GetTicketListById(edit.TicketsListId);
                var Car = await _CarRepository.GetCarByPlateNumber(edit.PlateNumber);
                ThatTicket.Car = Car; ThatTicket.CarId = Car.Id;
                ThatTicket.TicketsList = TicketList; ThatTicket.TicketDate = DateTime.Now;
                var result = await _TicketRepository.EditTicket(ThatTicket);

                if (result)
                {
                    return new TicketModifyResponse()
                    {
                        TicketId = ThatTicket.Id,PlateNumber=Car.PlateNumber,TicketsListId=ThatTicket.TicketsList.Id
                    };
                }
                else
                {
                    return null;
                }
        }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var response = await _TicketRepository.DeleteTicket(Id);
                return response;
            }
            catch
            {
                return false;
            }
        }
    }
}
