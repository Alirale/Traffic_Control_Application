
using Application.Services.Police;
using Core.ApiResponse;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Police")]
    public class TicketsListController : ControllerBase
    {
        private readonly ITicketCrudService _ticketCrudService;
        public TicketsListController(ITicketCrudService ticketCrudService)
        {
            _ticketCrudService = ticketCrudService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTicketList()
        {
            var result = await _ticketCrudService.GetAll();
            if (result.Count != 0)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("There is no any TicketList in database");
            }
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetTicketListbyId([FromQuery] int id)
        {
            var result = await _ticketCrudService.Get(id);
            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("No TicketList Item found with this information");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddTicketList([FromBody] AddTicketDTO ticket)
        {
            var result = await _ticketCrudService.Add(ticket);
            if (result != null)
            {
                return new ApiResponse().ModificationDone("TicketList Item succfully Added", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Added TicketList Item");
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditTicketList([FromBody] EditTicketDTO ticket)
        {
            var result = await _ticketCrudService.Edit(ticket);
            if (result)
            {
                return new ApiResponse().ModificationDone("TicketList Item succfully editted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not edit TicketList Item");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteTicketList([FromQuery] int id)
        {
            var result = await _ticketCrudService.Delete(id);
            if (result)
            {
                return new ApiResponse().ModificationDone("TicketList Item succesfully Deleted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Delete TicketList Item");
            }
        }
    }
}
