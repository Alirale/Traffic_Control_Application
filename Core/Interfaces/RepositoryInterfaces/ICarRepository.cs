using Core.Entities.Police;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByPlateNumber(string PlateNumber);
        public Car GetCarByPlateNumberForSpeedCam(string PlateNumber);
        public Task<List<Car>> GetAllCars();
        public Task<Car> GetCarById(int id);
        public Task<bool> AddCar(Car NewCar);
        public Task<bool> EditCar(Car NewCar);
        public Task<bool> DeleteCar(int Id);
    }
}
