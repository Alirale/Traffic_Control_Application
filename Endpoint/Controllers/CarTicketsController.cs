using Application.Services.Police;
using Core.ApiResponse;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Endpoint.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CarTicketsController : ControllerBase
    {
        private readonly ITicketService _carCrudService;
        public CarTicketsController(ITicketService carCrudService)
        {
            _carCrudService = carCrudService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _carCrudService.GetAll();
            if (result.Count != 0)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("There is no any Cartickets in database");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _carCrudService.Get(id);
            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("No CarTicket found with this information");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCarTicketDTO ticket)
        {
            var result = await _carCrudService.Add(ticket);
            if (result != null)
            {
                return new ApiResponse().ModificationDone("CarTicket succfully Added", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Added CarTicket");
            }
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditCarTicketDTO ticket)
        {
            var result = await _carCrudService.Edit(ticket);
            if (result != null)
            {
                return new ApiResponse().ModificationDone("CarTicket succfully editted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not edit CarTicket");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _carCrudService.Delete(id);
            if (result)
            {
                return new ApiResponse().ModificationDone("CarTicket succesfully Deleted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Delete CarTicket");
            }
        }
    }
}