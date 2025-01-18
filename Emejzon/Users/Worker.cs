using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Worker : User, IOrderManagement
    {
        public string Position { get; set; }
        public Worker(int id, string? name, string? surname, string? email, string position) : base(id, name, surname, email)
        {
            Position = position;
        }
    }
}
