using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    internal class Client : User, IClient
    {
        public int PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public Client(int id, string? name, Position position) :base(id,name,position) {}

        public void ClientMenu(int Id)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("[1]. Show status of your orders");
                Console.WriteLine("[2]. Remove order");
                Console.WriteLine("[3]. Place order");
                Console.WriteLine("[9]. Exit program");
                Console.WriteLine();
                Console.Write("Select option: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        IClient.ShowOrdersStatus(Id);
                        break;
                    case 2:
                        IClient.RemoveOrder(Id);
                        break;
                    case 3:
                        IClient.PlaceOrder(Id);
                        break;
                    case 9:
                        Console.WriteLine("See you next time");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Choose a valid option");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
