using Core.Entities.Police;
using System;

namespace Background.Entities.Background
{
    public class DetectedCarBySpeedCam
    {
        public int Id { get; set; }
        public Car DetectedCar { get; set; }
        public int DetectedCarId { get; set; }
        public DateTime DetectTime { get; set; }
        public SpeedCamera SpeedCamera { get; set; }

    }
}
