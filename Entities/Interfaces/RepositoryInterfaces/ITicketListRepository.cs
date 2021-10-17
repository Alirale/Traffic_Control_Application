using Core.Entities.Police;
using Core.Models;
using Core.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ITicketListRepository
    {
        public Task<List<TicketListDTO>> GetAllTicketLists();
        public Task<TicketListDTO> GetTicketListDTOById(int id);
        public Task<TicketsList> GetTicketListById(int id);
        public Task<bool> AddTicketList(AddTicketDTO NewTicket);
        public Task<bool> EditTicketList(EditTicketDTO edit);
        public Task<bool> DeleteTicketList(int Id);
    }
}
