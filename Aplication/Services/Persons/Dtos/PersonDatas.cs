using System.Collections.Generic;

namespace Aplication.Services.Persons.Dtos
{
    public class PersonDatas
    {
        public string CarOwner { get; set; }
        public string CarName { get; set; }
        public string PlateNumber { get; set; }
        public List<CarTickets> Tickets { get; set; }
    }
}
