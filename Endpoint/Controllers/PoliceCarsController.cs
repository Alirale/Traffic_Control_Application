using Aplication.Services.Police;
using Aplication.Services.Police.Dtos;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/<PoliceCarsController>
        [HttpGet]
        public object Get()
        {
            var OutPut = _carCrudService.GetAll();
            return OutPut;
        }

        // GET api/<PoliceCarsController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _carCrudService.Get(id);
        }

        // POST api/<PoliceCarsController>
        [HttpPost]
        public object Post(AddCarTicket ticket)
        {
            return _carCrudService.Add(ticket);
        }

        // PUT api/<PoliceCarsController>/5
        [HttpPut("{id}")]
        public object Put(EditCarTicket ticket)
        {
            return _carCrudService.Edit(ticket);
        }

        // DELETE api/<PoliceCarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _carCrudService.Delete(id);
        }
    }
}
