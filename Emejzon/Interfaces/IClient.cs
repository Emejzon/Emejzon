using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Emejzon.Services;

namespace Emejzon.Interfaces
{
    internal interface IClient
    {
        public static void PlaceOrder(int clientId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var insert = new MySqlCommand($"Insert into orders (ClientId) values ({clientId});", DB.Conn);
                insert.ExecuteNonQuery();
                using var select = new MySqlCommand($"Select max(Id) from orders where ClientId = {clientId};", DB.Conn);
                using var reader = select.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.DisposeAsync();

                while (true)
                {
                    Console.WriteLine("Insert product id: ");
                    var productId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Insert quantity: ");
                    var quantity = int.Parse(Console.ReadLine());

                    using var insertOrder = new MySqlCommand($"Insert into orderproducts(OrderId, ProductId, Quantity) values ({id}, {productId}, {quantity});", DB.Conn);
                    insertOrder.ExecuteNonQuery();

                    Console.WriteLine("Do you want to add another product? (y/n)");
                    var answer = Console.ReadLine();
                    if (answer == "n")
                    {
                        break;
                    }
                }
                LogManager.AddLogEntry(clientId, "Order placed");
                Console.WriteLine("Order placed");
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public static void ShowOrdersStatus(int clientId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand($"Select * from orders where ClientId = {clientId};", DB.Conn);
                using var reader = select.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Order id: {reader["Id"]}");
                }
                Console.ReadKey();
                Console.Clear();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public static void RemoveOrder(int clientId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert order id: ");
                var orderId = int.Parse(Console.ReadLine());

                using var select = new MySqlCommand($"Select * from orders where Id = {orderId} and ClientId = {clientId};", DB.Conn);
                using var reader = select.ExecuteReader();
                if (!reader.Read())
                {
                    Console.WriteLine("Order not found");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    reader.Close();
                    using var deleteProducts = new MySqlCommand($"Delete from orderproducts where OrderId = {orderId};", DB.Conn);
                    deleteProducts.ExecuteNonQuery();
                    using var delete = new MySqlCommand($"Delete from orders where Id = {orderId} and ClientId = {clientId};", DB.Conn);
                    delete.ExecuteNonQuery();
                }

                LogManager.AddLogEntry(clientId, $"Order with id {orderId} removed");
                Console.WriteLine($"Order with id {orderId} removed");
                Console.ReadKey();
                Console.Clear();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}