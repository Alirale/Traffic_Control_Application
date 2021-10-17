using System.Collections.Generic;

namespace TrafficControl.Core.Entities.Police
{
    public class CarsList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MaxSpeed { get; set; }
        public double CarLength { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
