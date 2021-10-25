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
    public class TicketRepository : ITicketRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public TicketRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            try
            {
                var Tickets = await _context.tickets.Include(x => x.Car).ThenInclude(x => x.Owner).Include(x => x.TicketsList)
                    .Include(x => x.Car).ThenInclude(x => x.carsList).AsNoTracking().ToListAsync();
                return Tickets;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Ticket> GetTicketDTOById(int id)
        {
            try
            {
                var Ticket = await _context.tickets.Include(x => x.Car).ThenInclude(x => x.Owner).Include(x => x.TicketsList)
                    .Include(x => x.Car).ThenInclude(x => x.carsList).Include(x => x.Car).ThenInclude(x => x.CarInHighway).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ticket;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TicketDTO> GetTicketDTOByKeyLessInfo(IdLessTicket ticket)
        {
            try
            {
                var Tickets = await _context.tickets.Include(x => x.Car).ThenInclude(x => x.Owner).Include(x => x.TicketsList)
                    .Include(x => x.Car).ThenInclude(x => x.carsList).Include(x => x.Car).ThenInclude(x => x.CarInHighway).AsNoTracking().FirstOrDefaultAsync(m => m.Car.Id == ticket.CarId && m.TicketDate == ticket.TicketDate && m.TicketsList == ticket.TicketsList);
                var OutputList = _mapper.Map<TicketDTO>(Tickets);
                return OutputList;
            }
            catch
            {
                return null;
            }
        }

        public TicketDTO GetTicketDTOByKeyLessInfoForSpeedCam(IdLessTicket ticket)
        {
            try
            {
                var Tickets = _context.tickets.Include(x => x.Car).ThenInclude(x => x.Owner).Include(x => x.TicketsList)
                    .Include(x => x.Car).ThenInclude(x => x.carsList).Include(x => x.Car).ThenInclude(x => x.CarInHighway).FirstOrDefault(m => m.Car.Id == ticket.CarId && m.TicketDate == ticket.TicketDate && m.TicketsList == ticket.TicketsList);
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
                var Tickets = await _context.tickets.Include(x => x.Car).ThenInclude(x => x.Owner).Include(x => x.TicketsList)
                    .Include(x => x.Car).ThenInclude(x => x.carsList).Include(x => x.Car).ThenInclude(x => x.CarInHighway).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
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

        public bool AddTicketForSpeedCam(Car NewCar)
        {
            _context.cars.Update(NewCar);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> EditTicket(Ticket EdittedTicket)
        {
            var Ticket = new Ticket() { CarId = EdittedTicket.CarId, Id = EdittedTicket.Id, TicketDate = EdittedTicket.TicketDate, TicketsList = EdittedTicket.TicketsList };
            _context.tickets.Update(Ticket);
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