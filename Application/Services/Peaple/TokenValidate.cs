using Core.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services.Peaple
{
    public interface ITokenValidator
    {
        Task Execute(TokenValidatedContext context);
    }
    public class TokenValidate : ITokenValidator
    {
        private IAccountRepository _acountRepository;
        public TokenValidate(IAccountRepository acountRepository)
        {
            _acountRepository = acountRepository;
        }
        public async Task Execute(TokenValidatedContext context)
        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsidentity?.Claims == null || !claimsidentity.Claims.Any())
            {
                context.Fail("claims not found....");
                return;
            }

            var userId = claimsidentity.FindFirst("Id").Value;
            if (userId != null)
            {
                context.Fail("claims not found....");
                return;
            }

            var user = _acountRepository.GetPersonById(int.Parse(userId));


            if (!(context.SecurityToken is JwtSecurityToken Token)
               || !await _acountRepository.CheckExistToken(Token.RawData))
            {
                context.Fail("Token not found in database");
                return;
            }


        }
    }
}
