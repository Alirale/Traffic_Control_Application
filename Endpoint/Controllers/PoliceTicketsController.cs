
using Application.Services.Police;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceTicketsController : ControllerBase
    {
        private readonly ITicketCrudService _ticketCrudService;
        public PoliceTicketsController(ITicketCrudService ticketCrudService)
        {
            _ticketCrudService = ticketCrudService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTicketList()
        {
            var result = await _ticketCrudService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid("There is no any TicketList in database");
            }
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTicketListbyId([FromRoute] int Id)
        {
            var result = await _ticketCrudService.Get(Id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid("No TicketList Item found with this information");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddTicketList([FromBody] AddTicketDTO ticket)
        {
            var result = await _ticketCrudService.Add(ticket);
            if (result)
            {
                return Ok("TicketList Item succfully Added");
            }
            else
            {
                return Forbid("Could not Added TicketList Item");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditTicketList([FromBody] EditTicketDTO ticket)
        {
            var result = await _ticketCrudService.Edit(ticket);
            if (result)
            {
                return Ok("TicketList Item succfully editted");
            }
            else
            {
                return Forbid("Could not edit TicketList Item");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketList([FromRoute] int id)
        {
            var result = await _ticketCrudService.Delete(id);
            if (result)
            {
                return Ok("TicketList Item succesfully Deleted");
            }
            else
            {
                return Forbid("Could not Delete TicketList Item");
            }
        }
    }
}
