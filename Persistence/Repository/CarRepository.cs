using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class CarRepository: ICarRepository
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
            var Car = await _context.cars.Include(x => x.Tickets).ThenInclude(x => x.TicketsList).Include(x=>x.Owner)
                .Include(x=>x.carsList).FirstOrDefaultAsync(x=>x.PlateNumber== PlateNumber);
            return Car;
        }
    }
}
