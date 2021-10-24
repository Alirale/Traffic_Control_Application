using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.SQL.Repository
{
    public class CarsListRepository : ICarsListRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public CarsListRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarsList> GetCarslistByName(string Name)
        {
            var Carslist = await _context.CarsLists.FirstOrDefaultAsync(x => x.Name == Name);
            return Carslist;
        }
    }
}
