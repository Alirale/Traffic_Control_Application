using System;

namespace Aplication.Services.Police.Dtos
{
    public class GetAllCarsTicketsDTO
    {
        public int TicketId { get; set; }
        public String Name { get; set; }
        public float Price { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
