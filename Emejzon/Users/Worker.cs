using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Emejzon.Interfaces;

namespace Emejzon.Users
{
    enum Role
    {
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

        public void WorkersMenu(Role role, int id)
        {
            switch (role)
            {
                case Role.Admin:
                    AdminMenu(id);
                    break;
                case Role.Manager:
                    ManagerMenu(id);
                    break;
                case Role.Worker:
                    WorkerMenu(id);
                    break;
            }
        }

        public void AdminMenu(int id)
        {
            Console.Clear();
            Console.WriteLine("\tWarehouse Management: ");
            Console.WriteLine("[11]. Add product to the warehouse");
            Console.WriteLine("[12]. Delete product from the warehouse");
            Console.WriteLine("[13]. Refill products");
            Console.WriteLine("[14]. Show all products");
            Console.WriteLine("[15]. Show all assigned orders");
            Console.WriteLine("[16]. Show all unassigned orders");
            Console.WriteLine("[17]. Asign order to a worker");

            Console.WriteLine(); //for visibility

            Console.WriteLine("\tUser management:");
            Console.WriteLine("[21]. Add user");
            Console.WriteLine("[22]. Modify user");
            Console.WriteLine("[23]. Delete user");
            Console.WriteLine("[24]. Show logs");
            Console.WriteLine(); //for visibility
            Console.WriteLine("[9]. Exit program");
            try
            {
                Console.Write("Select option: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 11:
                        IWarehouseManagement.AddProduct(id);
                        break;
                    case 12:
                        IWarehouseManagement.DeleteProduct(id);
                        break;
                    case 13:
                        IWarehouseManagement.RefillProduct(id);
                        break;
                    case 14:
                        IWarehouseManagement.ShowAllProducts();
                        break;
                    case 15:
                        IWarehouseManagement.ShowAllAssignedOrders();
                        break;
                    case 16:
                        IWarehouseManagement.ShowAllUnassignedOrders();
                        break;
                    case 17:
                        IWarehouseManagement.AsignOrder(id);
                        break;
                    case 21:
                        ISystemManagement.AddUser(id);
                        break;
                    case 22:
                        ISystemManagement.ModifyUser();
                        break;
                    case 23:
                        ISystemManagement.DeleteUser(id);
                        break;
                    case 24:
                        ISystemManagement.ShowLogs();
                        break;
                    case 9:
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

        public void ManagerMenu(int id)
        {
            Console.Clear();
            Console.WriteLine("\tManager Menu:");
            Console.WriteLine("[1]. Add product to the warehouse");
            Console.WriteLine("[2]. Delete product from the warehouse");
            Console.WriteLine("[3]. Refill products");
            Console.WriteLine("[4]. Show all products");
            Console.WriteLine("[5]. Show all assigned orders");
            Console.WriteLine("[6]. Show all unassigned orders");
            Console.WriteLine("[7]. Asign order to a worker");
            Console.WriteLine(); //for visibility
            Console.WriteLine("[9]. Exit program");
            try
            {
                Console.Write("Select option: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        IWarehouseManagement.AddProduct(id);
                        break;
                    case 2:
                        IWarehouseManagement.DeleteProduct(id);
                        break;
                    case 3:
                        IWarehouseManagement.RefillProduct(id);
                        break;
                    case 4:
                        IWarehouseManagement.ShowAllProducts();
                        break;
                    case 5:
                        IWarehouseManagement.ShowAllAssignedOrders();
                        break;
                    case 6:
                        IWarehouseManagement.ShowAllUnassignedOrders();
                        break;
                    case 7:
                        IWarehouseManagement.AsignOrder(id);
                        break;
                    case 9:
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

        public void WorkerMenu(int id)
        {
            Console.Clear();
            Console.WriteLine(" [1]. Display pending orders ");
            Console.WriteLine(" [2]. Finalize order");
            Console.WriteLine(" [3]. Send order");
            Console.WriteLine(); //for visibility
            Console.WriteLine(" [9]. Exit program");
            try
            {
                Console.Write("Select option: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        IOrderManagement.ShowAllWorkerAssignedOrders(id);
                        break;
                    case 2:
                        IOrderManagement.FinalizeOrder(id);
                        break;
                    case 3:
                        IOrderManagement.SendOrder(id);
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
