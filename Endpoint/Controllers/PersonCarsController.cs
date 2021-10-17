using Aplication.Services.Person;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] string Name)
        {
            var result = _getTickets.GetAllTickets(Name);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid("There is no any ticket in database");
            }
        }

    }
}
