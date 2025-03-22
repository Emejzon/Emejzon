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
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
        static void FinalizeOrder(int workerID)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert order ID to Finalize");
                int? orderID = int.Parse(Console.ReadLine());

                using var select = new MySqlCommand($"SELECT * FROM orders where Workerid = {workerID}", DB.Conn);
                using var reader = select.ExecuteReader();
                if (reader.Read())
                {
                    using var update = new MySqlCommand("Update orders set status = \"Finalized\"");
                    update.ExecuteNonQuery();
                    Console.WriteLine($"Order with ID {orderID} finalized");
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderID} isn't asigned to you");
                }
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
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
                    using var update = new MySqlCommand("Update orders set status = \"Sent\"");
                    update.ExecuteNonQuery();
                    Console.WriteLine($"Order with ID {orderID} sent");
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderID} isn't asigned to you");
                }
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
    }
}