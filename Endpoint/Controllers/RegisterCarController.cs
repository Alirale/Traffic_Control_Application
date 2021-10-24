using Application.Services.Cars;
using Core.ApiResponse;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterCarController : Controller
    {
        private readonly ICarRegisterService _CarRegisterService;
        public RegisterCarController(ICarRegisterService CarRegisterService)
        {
            _CarRegisterService = CarRegisterService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _CarRegisterService.GetAll();
            if (result.Count != 0)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("There is no any Cars in database");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _CarRegisterService.Get(id);
            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("No Car found with this information");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterCarDTO Car)
        {
            var result = await _CarRegisterService.Add(Car);
            if (result != null)
            {
                return new ApiResponse().ModificationDone("Car succfully Added", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Added Car");
            }
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RegisterCarREsponseDTO Car)
        {
            var result = await _CarRegisterService.Edit(Car);
            if (result != null)
            {
                return new ApiResponse().ModificationDone("Car succfully editted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not edit Car");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _CarRegisterService.Delete(id);
            if (result)
            {
                return new ApiResponse().ModificationDone("Car succesfully Deleted", result);
            }
            else
            {
                return new ApiResponse().FailedToFind("Could not Delete Car");
            }
        }
    }
}
