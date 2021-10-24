using System.Collections.Generic;

namespace Core.Entities.Police
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<Person> Person { get; set; }

    }
}
