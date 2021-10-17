
using Application.Services.Police;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Endpoint.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PoliceCarsController : ControllerBase
    {
        private readonly ICarCrudService _carCrudService;
        public PoliceCarsController(ICarCrudService carCrudService)
        {
            _carCrudService = carCrudService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _carCrudService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid("There is no any ticket in database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromBody] int id)
        {
            var result = _carCrudService.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid("No Ticket found with this information");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddCarTicketDTO ticket)
        {
            var result = await _carCrudService.Add(ticket);
            if (result)
            {
                return Ok("Ticket succfully Added");
            }
            else
            {
                return Forbid("Could not Added Ticket");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] EditCarTicketDTO ticket)
        {
            var result = await _carCrudService.Edit(ticket);
            if (result)
            {
                return Ok("Ticket succfully editted");
            }
            else
            {
                return Forbid("Could not edit Ticket");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _carCrudService.Delete(id);
            if (result)
            {
                return Ok("Ticket succesfully Deleted");
            }
            else
            {
                return Forbid("Could not Delete Ticket");
            }

        }
    }
}