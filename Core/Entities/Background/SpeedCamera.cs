using System.Collections.Generic;

namespace Core.Entities.Background
{
    public class SpeedCamera
    {
        public int Id { get; set; }
        public ICollection<DetectedCarBySpeedCam> DetectedCars { get; set; }
        public Highway Highway { get; set; }
        public int HighWayId { get; set; }
        public double Location { get; set; }
    }
}