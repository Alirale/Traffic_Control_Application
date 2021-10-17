using Core.Entities.Police;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        public Task<Person> GetPersonByName(string Name);
    }
}
