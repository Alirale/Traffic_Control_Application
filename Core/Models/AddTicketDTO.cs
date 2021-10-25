using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class AddTicketDTO

    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
