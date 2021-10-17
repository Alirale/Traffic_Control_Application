using System;
using System.Collections.Generic;

namespace Core.Entities.Background
{
    public class Highway
    {
        public int Id { get; set; }
        public String HighWayDirection { get; set; }
        public string Wheather { get; set; }
        public ICollection<CarInHighway> CarInHighWays { get; set; }
        public ICollection<SpeedCamera> SpeedCameras { get; set; }
        public double MaxAllowedSpeed { get; set; }
    }
}