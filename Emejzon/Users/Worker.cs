using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    enum Role{
        Worker,
        Admin,
        Manager
    }
    internal class Worker : User, IOrderManagement, ISystemManagement, IWarehouseManagement
    {
        public Role Role { get; set; }
        public Worker(int id, string? name, Position position, Role role) : base(id, name, position)
        {
            Role = role;
        }
    }
}
