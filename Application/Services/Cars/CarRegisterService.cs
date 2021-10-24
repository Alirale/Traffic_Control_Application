using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Cars
{
    public interface ICarRegisterService
    {
        public Task<List<RegisterCarREsponseDTO>> GetAll();
        public Task<RegisterCarREsponseDTO> Get(int Id);
        public Task<RegisterCarREsponseDTO> Add(RegisterCarDTO NewCar);
        public Task<RegisterCarREsponseDTO> Edit(RegisterCarREsponseDTO EdditedCar);
        public Task<bool> Delete(int Id);
    }

    public class CarRegisterService : ICarRegisterService
    {
        private readonly ICarRepository _carRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICarsListRepository _carsListRepository;
        private readonly IMapper _mapper;
        public CarRegisterService(ICarRepository carRepository, IMapper mapper, IPersonRepository personRepository, ICarsListRepository carsListRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _personRepository = personRepository;
            _carsListRepository = carsListRepository;
        }

        public async Task<List<RegisterCarREsponseDTO>> GetAll()
        {
            List<RegisterCarREsponseDTO> Output = new List<RegisterCarREsponseDTO>();
            var Tickets = await _carRepository.GetAllCars();
            Tickets.ForEach(Car =>
            {
                var CarDTO = _mapper.Map<RegisterCarREsponseDTO>(Car);
                Output.Add(CarDTO);
            });
            return Output;
        }

        public async Task<RegisterCarREsponseDTO> Get(int Id)
        {
            var Car = await _carRepository.GetCarById(Id);
            var output = _mapper.Map<RegisterCarREsponseDTO>(Car);
            return output;
        }

        public async Task<RegisterCarREsponseDTO> Add(RegisterCarDTO NewCar)
        {
            try
            {
                var Carowner = await _personRepository.GetPersonByName(NewCar.Owner.OwnerName);
                var Carslist = await _carsListRepository.GetCarslistByName(NewCar.CarsList.CarName);
                var Car = new Car()
                {
                    Owner = Carowner,
                    OwnerId = Carowner.Id,
                    carsList = Carslist,
                    carsListId = Carslist.Id,
                    PlateNumber = NewCar.PlateNumber,
                };
                var result = await _carRepository.AddCar(Car);
                var CreatedCar = await _carRepository.GetCarByPlateNumber(NewCar.PlateNumber);

                if (result && CreatedCar != null)
                {
                    return new RegisterCarREsponseDTO()
                    {
                        CarId = CreatedCar.Id,
                        Owner = new OwnerDTO() { OwnerName = CreatedCar.Owner.Name },
                        PlateNumber = CreatedCar.PlateNumber,
                        CarsList = new CarslistDTO() { CarName = CreatedCar.carsList.Name }
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<RegisterCarREsponseDTO> Edit(RegisterCarREsponseDTO EdditedCar)
        {
            try
            {
                var NewCarowner = await _personRepository.GetPersonByName(EdditedCar.Owner.OwnerName);
                var NewCarslist = await _carsListRepository.GetCarslistByName(EdditedCar.CarsList.CarName);
                var Car = await _carRepository.GetCarById(EdditedCar.CarId);

                Car.Owner = NewCarowner;
                Car.PlateNumber = EdditedCar.PlateNumber;
                Car.carsList = NewCarslist;

                var result = await _carRepository.EditCar(Car);
                if (result)
                {
                    return new RegisterCarREsponseDTO()
                    {
                        CarId = Car.Id,
                        Owner = new OwnerDTO() { OwnerName = Car.Owner.Name },
                        PlateNumber = Car.PlateNumber,
                        CarsList = new CarslistDTO() { CarName = Car.carsList.Name }
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var response = await _carRepository.DeleteCar(Id);
                return response;
            }
            catch
            {
                return false;
            }
        }
    }
}
