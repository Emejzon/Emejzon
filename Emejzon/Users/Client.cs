using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Client : User , IClient
    {
        public int PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public Client(int id, string? name, Position position) :base(id,name,position) {}

    }
}
