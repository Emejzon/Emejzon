using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Emejzon.Services;

namespace Emejzon.Interfaces
{
    internal interface IWarehouseManagement
    {
        public static void AddProduct(int userId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert product name: ");
                string? name = Console.ReadLine();
                Console.WriteLine("Insert product quantity: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Insert product category: ");
                string? category = Console.ReadLine();

                using var insert = new MySqlCommand($"Insert into products(Name,Quantity,Category) values (\"{name}\",{quantity},\"{category}\");", DB.Conn);
                insert.ExecuteNonQuery();

                Console.WriteLine($"Added {name} to product list");
                LogManager.AddLogEntry(userId, $"Added {name} to product list");
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void DeleteProduct(int userId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert product id to delete: ");
                int id = int.Parse(Console.ReadLine());

                using var delete = new MySqlCommand($"Delete from products where id = {id}", DB.Conn);
                if(delete.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("Product not found");
                }
                else{
                    Console.WriteLine($"Deleted product with id {id}");
                    LogManager.AddLogEntry(userId, $"Deleted product with id {id}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void RefillProduct(int userId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert product id to refill: ");
                int? id = int.Parse(Console.ReadLine());
                Console.WriteLine("Insert quantity to add: ");
                int? quantity = int.Parse(Console.ReadLine());

                using var update = new MySqlCommand($"UPDATE products SET quantity = quantity + {quantity} WHERE id = {id};", DB.Conn);
                if(update.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("Product not found");
                }
                else
                {
                    Console.WriteLine($"Refilled product with id {id}");
                    LogManager.AddLogEntry(userId, $"Refilled product with id {id}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void ShowAllProducts()
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand("SELECT * FROM products", DB.Conn);
                using var reader = select.ExecuteReader();

                Console.WriteLine("ID | Name | Quantity | Category");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} | {reader.GetString(1)} | {reader.GetInt64(2)} | {reader.GetString(3)}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void ShowAllOrders()
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand("SELECT * FROM orders", DB.Conn);
                using var reader = select.ExecuteReader();

                Console.WriteLine("ID | ClientID | WorkerID | Status");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} | {reader.GetInt64(1)} | {reader.GetInt64(2)} | {reader.GetString(4)}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void ShowAllAssignedOrders()
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand("SELECT * FROM orders where Workerid IS NOT NULL", DB.Conn);
                using var reader = select.ExecuteReader();

                Console.WriteLine("ID | ClientID | WorkerID | Status");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} | {reader.GetInt64(1)} | {reader.GetInt64(2)} | {reader.GetBoolean(3)}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void ShowAllUnassignedOrders()
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var command = new MySqlCommand("SELECT * FROM orders where Workerid = null", DB.Conn);
                using var reader = command.ExecuteReader();

                Console.WriteLine("ID | ClientID | Status");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} | {reader.GetInt64(1)} | {reader.GetString(4)}");
                }
                Console.ReadKey();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        public static void AsignOrder(int userId)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert orderId: ");
                int? orderID = int.Parse(Console.ReadLine());
                Console.WriteLine("Insert workerId: ");
                int? workerID = int.Parse(Console.ReadLine());

                using var update = new MySqlCommand($"Update orders set workerID = {workerID},status = \"Processing\" where ID = {orderID}", DB.Conn);
                update.ExecuteNonQuery();

                Console.WriteLine($"Order with id {orderID} asigned to worker with id {workerID}");
                LogManager.AddLogEntry(userId, $"Order with id {orderID} asigned to worker with id {workerID}");
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

    }
}