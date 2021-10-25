using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Police
{
    public interface ITicketCrudService
    {
        public Task<List<TicketListDTO>> GetAll();
        public Task<TicketListDTO> Get(int Id);
        public Task<bool> Add(AddTicketDTO NewTicket);
        public Task<bool> Delete(int Id);
        public Task<bool> Edit(EditTicketDTO edit);
    }

    public class TicketCrudService : ITicketCrudService
    {
        private readonly ITicketListRepository _TicketListRepository;


        public TicketCrudService(ITicketListRepository TicketListRepository)
        {
            _TicketListRepository = TicketListRepository;
        }

        public async Task<List<TicketListDTO>> GetAll()
        {
            return await _TicketListRepository.GetAllTicketLists();
        }

        public async Task<TicketListDTO> Get(int Id)
        {
            return await _TicketListRepository.GetTicketListDTOById(Id);
        }

        public async Task<bool> Add(AddTicketDTO NewTicket)
        {
            try
            {
                await _TicketListRepository.AddTicketList(NewTicket);
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
                await _TicketListRepository.DeleteTicketList(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Edit(EditTicketDTO edit)
        {
            try
            {
                await _TicketListRepository.EditTicketList(edit);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
