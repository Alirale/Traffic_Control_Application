using AutoMapper;
using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models.NewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficControl.Infrastructure.Repository
{
    public class PersonRepository: IPersonRepository
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
            var Persons = await _context.persons.Include(x=>x.Cars).ThenInclude(x=>x.Tickets)
                .ThenInclude(x=>x.TicketsList).Include(x=>x.Cars).ThenInclude(x=>x.carsList).ToListAsync();
            return await _context.persons.FirstOrDefaultAsync(x => x.Name == Name);
        }
    }
}
