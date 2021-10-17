
using Core.Entities.Police;
using System.Threading.Tasks;


namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByPlateNumber(string PlateNumber);
    }
}
