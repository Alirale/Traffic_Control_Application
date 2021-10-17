using System.Collections.Generic;

namespace Core.Models
{
    public class PersonDTO
    {
        public ICollection<CarDTO> Cars { get; set; }
    }
}
