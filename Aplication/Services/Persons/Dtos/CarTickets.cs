using Aplication.Services.Police.Dtos;
using Entities;
using System;

namespace Aplication.Services.Persons.Dtos
{
    public class CarTickets
    {
        public string TicketName { get; set; }
        public string TicketPrice { get; set; }
        public string TicketDate { get; set; }
        public GetallCarscarDTO Car { get; set; }
    }
}
