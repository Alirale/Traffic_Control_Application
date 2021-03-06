
using Core.Entities.Police;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ITicketRepository
    {
        public Task<List<Ticket>> GetAllTickets();
        public Task<Ticket> GetTicketDTOById(int id);
        public Task<Ticket> GetTicketById(int id);
        public Task<bool> AddTicket(Car NewCar);
        public bool AddTicketForSpeedCam(Car NewCar);
        public Task<bool> EditTicket(Ticket EditedTicket);
        public Task<bool> DeleteTicket(int Id);
        public Task<TicketDTO> GetTicketDTOByKeyLessInfo(IdLessTicket ticket);
        public TicketDTO GetTicketDTOByKeyLessInfoForSpeedCam(IdLessTicket ticket);
    }
}
