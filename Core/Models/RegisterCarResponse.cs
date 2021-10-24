using Core.Entities.Police;
using System;

namespace Core.Models
{
    public class RegisterCarResponse
    {
        public int Id { get; set; }
        public Person Owner { get; set; }
        public CarsList carsList { get; set; }
        public String PlateNumber { get; set; }
    }
}
