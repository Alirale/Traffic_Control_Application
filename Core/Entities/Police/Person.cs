using System.Collections.Generic;

namespace Core.Entities.Police
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public ICollection<Token> tokens { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
