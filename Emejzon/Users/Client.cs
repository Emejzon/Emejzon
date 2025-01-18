using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emejzon.Users
{
    internal class Client : User
    {
        public int PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public Client(int id, string? name, string? surname, string? email, int phoneNumber, string city, string address) :base(id,name,surname,email) 
        { 
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
        }
    }
}
