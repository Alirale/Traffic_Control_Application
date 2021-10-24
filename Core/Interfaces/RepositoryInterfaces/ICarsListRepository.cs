using Core.Entities.Police;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ICarsListRepository
    {
        public Task<CarsList> GetCarslistByName(string Name);
    }
}
