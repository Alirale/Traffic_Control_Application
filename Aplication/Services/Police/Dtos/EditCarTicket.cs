using System;

namespace Aplication.Services.Police.Dtos
{
    public class EditCarTicket
    {
        public int Id { get; set; }
        public String PlateNumber { get; set; }
        public int TicketsListId { get; set; }
    }
}
