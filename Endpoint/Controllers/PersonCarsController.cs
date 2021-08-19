using Aplication.Services.Persons;
using Microsoft.AspNetCore.Mvc;



namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonCarsController : ControllerBase
    {
        private readonly IGetTickets _getTickets;
        public PersonCarsController(IGetTickets getTickets)
        {
            _getTickets = getTickets;
        }

        // GET: api/<PersonCarsController>
        [HttpGet]
        public object Get(string Name)
        {
            return _getTickets.GetAllTickets(Name);
        }


    }
}
