using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Worker : User, IOrderManagement, ISystemManagement, IWarehouseManagement
    {
        public string Position { get; set; }
        public Worker(int id, string? name, string? surname, string? email, Role role, string position) : base(id, name, surname, email, role)
        {
            Position = position;
        }
    }
}
