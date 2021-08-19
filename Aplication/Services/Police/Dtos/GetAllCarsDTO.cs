using System;
using System.Collections.Generic;

namespace Aplication.Services.Police.Dtos
{
    public class GetAllCarsDTO
    {
        public int CarId { get; set; }
        public String Owner { get; set; }
        public string PlateNumber { get; set; }
        public String CarName { get; set; }
        public ICollection<GetAllCarsTicketsDTO> Tickets { get; set; }

    }
}
