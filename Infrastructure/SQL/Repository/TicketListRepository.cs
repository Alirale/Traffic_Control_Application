using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{

    public class TicketListRepository : ITicketListRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public TicketListRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TicketListDTO>> GetAllTicketLists()
        {
            try
            {
                var TicketLists = await _context.ticketsLists.AsNoTracking().ToListAsync();
                var OutputList = new List<TicketListDTO>();
                TicketLists.ForEach(x => OutputList.Add(_mapper.Map<TicketListDTO>(x)));
                return OutputList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TicketListDTO> GetTicketListDTOById(int Id)
        {
            try
            {
                var Ticket = await _context.ticketsLists.Include(x => x.Tickets).FirstOrDefaultAsync(x => x.Id == Id);
                var OutputList = _mapper.Map<TicketListDTO>(Ticket);
                return OutputList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TicketsList> GetTicketListById(int id)
        {
            try
            {
                var Ticket = await _context.ticketsLists.Include(x => x.Tickets).FirstOrDefaultAsync(m => m.Id == id);
                return Ticket;
            }
            catch
            {
                return null;
            }
        }

        public TicketsList GetTicketListByIdForSpeedCam(int id)
        {
            try
            {
                var Ticket = _context.ticketsLists.Include(x => x.Tickets).FirstOrDefault(m => m.Id == id);
                return Ticket;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddTicketList(AddTicketDTO NewTicket)
        {
            await _context.ticketsLists.AddAsync(new TicketsList() { Name = NewTicket.Name, Price = NewTicket.Price });
            return await Save();
        }

        public async Task<bool> EditTicketList(EditTicketDTO edit)
        {
            var ticketsList = await _context.ticketsLists.FirstOrDefaultAsync(p => p.Id == edit.Id);
            ticketsList.Name = edit.Name;
            ticketsList.Price = edit.Price;
            return await Save();
        }

        public async Task<bool> DeleteTicketList(int Id)
        {
            var ticketlist = await _context.ticketsLists.FindAsync(Id);
            _context.ticketsLists.Remove(ticketlist);
            return await Save();
        }

        private async Task<bool> Save()
        {
            var Result = await _context.SaveChangesAsync();
            if (Result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
