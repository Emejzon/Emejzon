using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emejzon.Users
{
    enum Position{
        Worker,
        Client
    }
    abstract class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public Position Position { get; set; }
        public User(int id, string? name, Position position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
    }
}
