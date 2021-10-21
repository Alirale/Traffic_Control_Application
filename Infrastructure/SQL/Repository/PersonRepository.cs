using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Person> GetPersonByName(string Name)
        {
            var person = await _context.persons.Include(x => x.Cars).ThenInclude(x => x.Tickets)
                .ThenInclude(x => x.TicketsList).Include(x => x.Cars).ThenInclude(x => x.carsList).FirstOrDefaultAsync(x=>x.Name == Name);
            return person;
        }
        
    }
}
