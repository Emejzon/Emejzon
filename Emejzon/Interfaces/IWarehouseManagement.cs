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
            throw new NotImplementedException();
        }
        public static void DeleteProduct(){
            throw new NotImplementedException();
        }
        public static void RefillProduct(){
            throw new NotImplementedException();
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