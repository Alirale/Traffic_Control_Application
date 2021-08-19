using Aplication.Services.Police;
using Aplication.Services.Police.Dtos;
using Microsoft.AspNetCore.Mvc;



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

        // GET: api/<PoliceTicketsController>
        [HttpGet]
        public object Get()
        {
            return _ticketCrudService.GetAll();
        }

        // GET api/<PoliceTicketsController>/5
        [HttpGet("{Id}")]
        public object Get(int Id)
        {
            ;
            return _ticketCrudService.Get(Id);
        }

        // POST api/<PoliceTicketsController>
        [HttpPost]
        public object Post([FromBody] AddTicket ticket)
        {
            return _ticketCrudService.Add(ticket);
        }

        // PUT api/<PoliceTicketsController>/5
        [HttpPut("{id}")]
        public object Put([FromBody] EditTicket ticket)
        {
            return _ticketCrudService.Edit(ticket);
        }

        // DELETE api/<PoliceTicketsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ticketCrudService.Delete(id);
        }
    }
}
