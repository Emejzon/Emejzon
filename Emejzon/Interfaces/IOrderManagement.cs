using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Emejzon.Services;

namespace Emejzon.Interfaces
{
    internal interface IOrderManagement
    {
        static void ShowAllWorkerAssignedOrders(int workerID)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand($"SELECT * FROM orders where Workerid = {workerID}", DB.Conn);
                using var reader = select.ExecuteReader();

                Console.WriteLine("ID | ClientID | Status");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} | {reader.GetInt64(1)} | {reader.GetString(4)}");
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
        static void FinalizeOrder(int workerID)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert order ID to finalize:");
                if (!int.TryParse(Console.ReadLine(), out int orderID))
                {
                    Console.WriteLine("Invalid order ID.");
                    return;
                }

                // Check if the order is assigned to the worker
                using var selectOrder = new MySqlCommand($"SELECT * FROM orders WHERE WorkerId = {workerID} AND Id = {orderID}", DB.Conn);

                using var orderReader = selectOrder.ExecuteReader();
                if (orderReader.Read())
                {
                    orderReader.Close(); 

                    using var selectProducts = new MySqlCommand($"SELECT ProductId, Quantity FROM orderproducts WHERE OrderId = {orderID}", DB.Conn);
                    using var productReader = selectProducts.ExecuteReader();
                    var productUpdates = new List<(int productId, int quantity)>();

                    while (productReader.Read())
                    {
                        int productId = productReader.GetInt32(0);
                        int quantity = productReader.GetInt32(1);
                        productUpdates.Add((productId, quantity));
                    }
                    productReader.Close();

                    foreach (var (productId, quantity) in productUpdates)
                    {
                        using var updateProduct = new MySqlCommand($"UPDATE products SET Quantity = Quantity - {quantity} WHERE Id = {productId}", DB.Conn);
                        updateProduct.ExecuteNonQuery();
                    }

                    using var updateOrder = new MySqlCommand($"UPDATE orders SET Status = 'Finalized' WHERE Id = {orderID}", DB.Conn);
                    updateOrder.ExecuteNonQuery();

                    Console.WriteLine($"Order with ID {orderID} finalized.");
                    LogManager.AddLogEntry(workerID, $"Order with ID {orderID} finalized and product quantities updated.");
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderID} isn't asigned o you");
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
        static void SendOrder(int workerID)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert order ID to Finalize");
                int? orderID = int.Parse(Console.ReadLine());

                using var select = new MySqlCommand($"SELECT * FROM orders where Workerid = {workerID} and OrderID = {orderID}", DB.Conn);
                using var reader = select.ExecuteReader();
                if (reader.Read())
                {
                    using var update = new MySqlCommand("Update orders set status = \"Sent\"",DB.Conn);
                    update.ExecuteNonQuery();
                    Console.WriteLine($"Order with ID {orderID} sent");
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderID} isn't asigned to you");
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
    }
}