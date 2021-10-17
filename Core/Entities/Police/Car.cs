
using Core.Entities.Background;
using System;
using System.Collections.Generic;

namespace Core.Entities.Police
{
    public class Car
    {
        public int Id { get; set; }
        public Person Owner { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public CarsList carsList { get; set; }
        public String PlateNumber { get; set; }
        public CarInHighway CarInHighway { get; set; }
    }
}
