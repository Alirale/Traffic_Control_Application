using Core.Entities.Police;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        public Task<TokenResponseDTO> Register(RegisterInfosDTO dto);
        public Task<TokenResponseDTO> LoginwithUserPass(LoginDTO Person);
        public Task<TokenResponseDTO> LoginwithRefreshToken(string RefreshToken);
        public Task<Person> GetPersonById(int Id);
        public Task<bool> CheckExistToken(string Token);
    }
}
