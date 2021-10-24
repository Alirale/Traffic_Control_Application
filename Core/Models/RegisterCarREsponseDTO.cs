using System;

namespace Core.Models
{
    public class RegisterCarREsponseDTO
    {
        public int CarId { get; set; }
        public OwnerDTO Owner { get; set; }
        public CarslistDTO CarsList { get; set; }
        public String PlateNumber { get; set; }
    }
}
