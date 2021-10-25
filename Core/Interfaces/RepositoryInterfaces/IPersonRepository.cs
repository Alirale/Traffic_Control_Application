
using Core.Entities.Police;
using System.Threading.Tasks;


namespace Core.Interfaces.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        public Task<Person> GetPersonByName(string Name);
    }
}
