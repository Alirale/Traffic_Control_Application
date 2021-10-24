using System;

namespace Core.Entities.Police
{
    public class Token
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExp { get; set; }
        public Person User { get; set; }
        public int PersonId { get; set; }
    }
}
