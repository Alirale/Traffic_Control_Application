using Aplication.Services.Police.Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Police
{
    public interface ITicketCrudService
    {
        public List<GetTicketsListDTO> GetAll();
        public GetTicketsListDTO Get(int Id);
        public ReturningTicketListDTO Add(AddTicket NewTicket);
        public void Delete(int Id);
        public bool Edit(EditTicket edit);
    }

    public class TicketCrudService : ITicketCrudService
    {
        private readonly IDatabasecontextPolice _context;


        public TicketCrudService(IDatabasecontextPolice context)
        {
            _context = context;
        }

        public List<GetTicketsListDTO> GetAll()
        {
            List<GetTicketsListDTO> Ticketslist = new List<GetTicketsListDTO>();
            var Tickets = _context.ticketsList.Include(p => p.Tickets).ToList();
            foreach (var item in Tickets)
            {
                Ticketslist.Add(new GetTicketsListDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price
                });
            }
            return Ticketslist;
        }

        public GetTicketsListDTO Get(int Id)
        {
            var TicketsList = _context.ticketsList.SingleOrDefault(p => p.Id == Id);
            if (TicketsList != null)
            {
                return new GetTicketsListDTO()
                {
                    Id = TicketsList.Id,
                    Name = TicketsList.Name,
                    Price = TicketsList.Price
                };
            }
            else 
            {
                return null;
            }

        }

        public ReturningTicketListDTO Add(AddTicket NewTicket)
        {
            _context.ticketsList.AddAsync(new TicketsList() { Name = NewTicket.Name, Price = NewTicket.Price });
            _context.SaveChanges();
            
            var Confirm = _context.ticketsList.FirstOrDefault(p => p.Name == NewTicket.Name);
            return new ReturningTicketListDTO(){Id=Confirm.Id,Name=Confirm.Name,Price=Confirm.Price};

        }

        public void Delete(int Id)
        {
            _context.ticketsList.Remove(new TicketsList { Id = Id });
            _context.SaveChanges();
        }

        public bool Edit(EditTicket edit)
        {
            var ticketsList = _context.ticketsList.FirstOrDefault(p => p.Id == edit.Id);
            ticketsList.Name = edit.Name;
            ticketsList.Price = edit.Price;
            _context.SaveChanges();
            return true;
        }


    }
}
