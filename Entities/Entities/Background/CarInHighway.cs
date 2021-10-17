using Core.Entities.Police;
using System;

namespace Core.Entities.Background
{
    public class CarInHighway
    {
        public int Id { get; set; }
        public double Location { get; set; }
        public DateTime LastLocationChangeTime { get; set; }
        public Driver Driver { get; set; }
        public Highway Highway { get; set; }
        public int HighwayId { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}
