using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Manger : User, IOrderManagement, IWarehouseManagement
    {
        public string Position { get; set; }
        public Manger(int id, string? name, string? surname, string? email, string position) : base(id, name, surname, email)
        {
            Position = position;
        }
    }
}
