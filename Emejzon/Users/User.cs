using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emejzon.Users
{
    abstract class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public User(int id, string? name, string? surname, string? email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
