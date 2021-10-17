using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficControl.Core.Models.NewModels
{
    public class PersonDTO
    {
        public ICollection<CarDTO> Cars { get; set; }
    }
}
