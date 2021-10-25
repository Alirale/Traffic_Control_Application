using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public CarRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Car> GetCarByPlateNumber(string PlateNumber)
        {
            var Car = await _context.cars.Include(x => x.Tickets).ThenInclude(x => x.TicketsList).Include(x => x.Owner)
                .Include(x => x.carsList).FirstOrDefaultAsync(x => x.PlateNumber == PlateNumber);
            return Car;
        }

        public Car GetCarByPlateNumberForSpeedCam(string PlateNumber)
        {
            var Car = _context.cars.Include(x => x.Tickets).ThenInclude(x => x.TicketsList).Include(x => x.Owner)
                .Include(x => x.carsList).FirstOrDefault(x => x.PlateNumber == PlateNumber);
            return Car;
        }

        public async Task<List<Car>> GetAllCars()
        {
            try
            {
                var Cars = await _context.cars.Include(x => x.Owner).ThenInclude(x => x.Cars).ThenInclude(x => x.carsList)
                    .ToListAsync();
                return Cars;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Car> GetCarById(int id)
        {
            try
            {
                var Cars = await _context.cars.Include(x => x.Owner).ThenInclude(x => x.Cars).ThenInclude(x => x.carsList)
                    .FirstOrDefaultAsync(c => c.Id == id);
                return Cars;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> AddCar(Car NewCar)
        {
            _context.cars.Update(NewCar);
            return await Save();
        }

        public async Task<bool> EditCar(Car NewCar)
        {
            _context.cars.Update(NewCar);
            return await Save();
        }

        public async Task<bool> DeleteCar(int Id)
        {
            _context.cars.Remove(await _context.cars.FindAsync(Id));
            return await Save();
        }

        private async Task<bool> Save()
        {
            var Result = await _context.SaveChangesAsync();
            if (Result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
