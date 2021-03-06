using Core.Entities.Police;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ITicketListRepository
    {
        public Task<List<TicketListDTO>> GetAllTicketLists();
        public Task<TicketListDTO> GetTicketListDTOById(int id);
        public Task<TicketsList> GetTicketListById(int id);
        public TicketsList GetTicketListByIdForSpeedCam(int id);
        public Task<bool> AddTicketList(AddTicketDTO NewTicket);
        public Task<bool> EditTicketList(EditTicketDTO edit);
        public Task<bool> DeleteTicketList(int Id);
    }
}
