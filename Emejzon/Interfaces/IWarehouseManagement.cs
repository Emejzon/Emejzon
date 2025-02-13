using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Emejzon.Interfaces
{
    internal interface IWarehouseManagement
    {
        public static void AddProduct(){
            Console.WriteLine("Insert product name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("Insert product quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert product category: ");
            string? category = Console.ReadLine();

            using var conn1 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn1.Open();
            using var insert = new MySqlCommand($"Insert into products(Name,Quantity,Category) values (\"{name}\",{quantity},\"category\");", conn1);
            insert.ExecuteNonQuery();
            conn1.Dispose();

            Console.WriteLine($"Added {name} to product list");
        }
        public static void DeleteProduct(){
            Console.WriteLine("Insert product id to delete: ");
            int id = int.Parse(Console.ReadLine());

            using var conn1 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn1.Open();
            using var delete = new MySqlCommand($"Delete from products where id = {id}",conn1);
            delete.ExecuteNonQuery();

            Console.WriteLine($"Deleted product with id {id}");
        }
        public static void RefillProduct(){
            Console.WriteLine("Insert product id to refill: ");
            int? id = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert quantity to add: ");
            int? quantity = int.Parse(Console.ReadLine());

            using var conn1 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn1.Open();
            using var update = new MySqlCommand($"UPDATE products SET quantity = quantity + {quantity} WHERE id = {id};");
            update.ExecuteNonQuery();
            conn1.Dispose();

            Console.WriteLine($"Refilled product with id {id}");
        }
        public static void ShowAllProducts(){
            using var conn = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn.Open();
            using var command = new MySqlCommand("SELECT * FROM products where hidden = 0",conn);
            using var reader = command.ExecuteReader();

            while(reader.Read()){
                Console.WriteLine($"{reader.GetString(0)}| Quantity: {reader.GetInt64(1)}| Category: {reader.GetString(2)}");
            }
        }
        public static void ShowAllOrders(){
            throw new NotImplementedException();
        }
        public static void ShowAllAssignedOrders(){
            throw new NotImplementedException();
        }
        public static void ShowAllUnassignedOrders(){
            throw new NotImplementedException();
        }

        
    }
}