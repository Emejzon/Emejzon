using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Admin : User, IOrderManagement, ISystemManagement
    {
        public string Position { get; set; }
        public Admin(int id, string? name, string? surname, string? email, string position) : base(id, name, surname, email)
        {
            Position = position;
        }
    }
}
