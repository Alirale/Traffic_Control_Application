using System;

namespace Core.Models
{
    public class RegisterCarDTO
    {
        public OwnerDTO Owner { get; set; }
        public CarslistDTO CarsList { get; set; }
        public String PlateNumber { get; set; }
    }
}
