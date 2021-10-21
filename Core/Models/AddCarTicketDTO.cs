using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class AddCarTicketDTO
    {
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public int TicketsListId { get; set; }
    }
}
