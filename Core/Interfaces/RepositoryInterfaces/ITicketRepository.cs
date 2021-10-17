
using Core.Entities.Police;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ITicketRepository
    {
        public Task<List<TicketDTO>> GetAllTickets();
        public Task<TicketDTO> GetTicketDTOById(int id);
        public Task<Ticket> GetTicketById(int id);
        public Task<bool> AddTicket(Car NewCar);
        public Task<bool> EditTicket(Ticket EditedTicket);
        public Task<bool> DeleteTicket(int Id);
    }
}
