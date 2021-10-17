using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models.NewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrafficControl.Infrastructure.Repository
{
    public class TicketRepository: ITicketRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public TicketRepository(DatabaseContext context, IMapper mapper ,)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TicketDTO>> GetAllTickets()
        {
            try
            {
                var Tickets = await _context.tickets.ToListAsync();
                var OutputList = new List<TicketDTO>();
                OutputList.ForEach(x => OutputList.Add(_mapper.Map<TicketDTO>(Tickets)));
                return OutputList;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<TicketDTO> GetTicketDTOById(int id)
        {
            try
            {
                var Tickets = await _context.tickets.Include(x => x.TicketsList).FirstOrDefaultAsync(m => m.Id == id);
                var OutputList = _mapper.Map<TicketDTO>(Tickets);
                return OutputList;
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<Ticket> GetTicketById(int id)
        {
            try
            {
                var Tickets = await _context.tickets.Include(x=>x.TicketsList).FirstOrDefaultAsync(m => m.Id == id);
                return Tickets;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddTicket(Car NewCar)
        {
            _context.cars.Update(NewCar);
            return await Save();
        }

        public async Task<bool> EditTicket(Ticket EdittedTicket)
        {
            _context.tickets.Update(EdittedTicket);
            return await Save();
        }

        public async Task<bool> DeleteTicket(int Id)
        {
            _context.tickets.Remove(await _context.tickets.FindAsync(Id));
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