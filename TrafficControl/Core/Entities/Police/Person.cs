using System;
using System.Collections.Generic;

namespace TrafficControl.Core.Entities.Police
{
    public class Person
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
