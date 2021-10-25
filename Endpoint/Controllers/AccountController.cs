using Core.ApiResponse;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("RegisterInfo")]
        public async Task<IActionResult> Register(RegisterInfosDTO register)
        {
            var loginResult = await _accountRepository.Register(register);
            if (loginResult != null)
            {
                return new ApiResponse().Success(loginResult);
            }
            else
            {
                return new ApiResponse().FailedToFind("Username already exist please use another name");
            }
        }

        [HttpPost("LoginInfo")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var loginResult = await _accountRepository.LoginwithUserPass(login);
            if (loginResult != null)
            {
                return new ApiResponse().Success(loginResult);
            }
            else
            {
                return new ApiResponse().FailedToFind("UserName or Password does not match. please try agin");
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> GetTokenByRefreshToken(string RefreshToken)
        {
            var loginResult = await _accountRepository.LoginwithRefreshToken(RefreshToken);
            if (loginResult != null)
            {
                return new ApiResponse().Success(loginResult);
            }
            else
            {
                return new ApiResponse().FailedToFind("Invalid RefreshToken");
            }
        }
    }
}
