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
        public Client(int id, string? name, Position position) : base(id, name, position) { }

        public void ClientMenu(int Id)
        {
            Console.Clear();
            Console.WriteLine("[1]. Show status of your orders");
            Console.WriteLine("[2]. Remove order");
            Console.WriteLine("[3]. Place order");
            Console.WriteLine("[4]. Show all products");
            Console.WriteLine(); //for visibility
            Console.WriteLine("[9]. Exit program");
            try
            {
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
                    case 4:
                        IWarehouseManagement.ShowAllProducts();
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
