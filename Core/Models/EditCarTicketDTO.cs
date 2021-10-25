using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class EditCarTicketDTO
    {
        [Required]
        public int TicketId { get; set; }
        [Required]
        public String PlateNumber { get; set; }
        [Required]
        public int TicketsListId { get; set; }
    }
}
