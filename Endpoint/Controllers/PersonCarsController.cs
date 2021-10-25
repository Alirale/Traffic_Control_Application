using Core.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrafficControl.Aplication.Services.Peaple;


namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Citizen,Police")]
    public class PersonCarsController : ControllerBase
    {
        private readonly IGetTickets _getTickets;
        public PersonCarsController(IGetTickets getTickets)
        {
            _getTickets = getTickets;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string Name)
        {
            var result = await _getTickets.GetAllTickets(Name);

            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("There is no any tickets in database");
            }

        }

    }
}
