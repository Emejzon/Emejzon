using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Services;
using MySqlConnector;

namespace Emejzon.Interfaces
{
    internal interface ISystemManagement
    {
        static void AddUser()
        {
            Console.WriteLine("Insert name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("Insert surname: ");
            string? surname = Console.ReadLine();
            Console.WriteLine("Insert phone number: ");
            int? num = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert city: ");
            string? city = Console.ReadLine();
            Console.WriteLine("Insert address: ");
            string? address = Console.ReadLine();
            Console.WriteLine("Insert email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Insert password: ");
            string password = PasswordManager.HashPassword(Console.ReadLine());
            Console.WriteLine("Insert position: ");
            string position = PasswordManager.HashPassword(Console.ReadLine());

            using var conn1 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn1.Open();
            using var command = new MySqlCommand("SELECT Email, PhoneNumber FROM users;", conn1);
            using var reader = command.ExecuteReader();
            conn1.Dispose();

            if (reader.HasRows)
            {
                using var conn2 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
                conn2.Open();
                using var newUser = new MySqlCommand($"INSERT INTO users(Name,Surname,PhoneNumber,City,Address,Email,PASSWORD,Position) VALUES (\"{name} \",\"{surname}\",\"{num}\",\"{city}\",\"{address}\",\"{email}\",\"{password}\",\"{position}\")", conn2);
                newUser.ExecuteNonQuery();
                conn2.Dispose();
            }
            else
            {
                Console.WriteLine("User with same email or phone number already exist!");
            }
        }
        static void ModifyUser()
        {
            throw new NotImplementedException();
        }
        static void DeleteUser()
        {
            Console.WriteLine("Insert email of user that you want to delete: ");
            string? email = Console.ReadLine();

            using var conn1 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn1.Open();
            using var command = new MySqlCommand($"Select Email from users where email = \"{email}\" ", conn1);
            using var reader = command.ExecuteReader();
            conn1.Dispose();

            if (reader.HasRows)
            {
                using var conn2 = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
                conn1.Open();
                using var remove = new MySqlCommand($"DELETE FROM users WHERE Email = \"{email}\" ", conn2);
                remove.ExecuteNonQuery();
                Console.WriteLine($"User with email {email} removed");
            }
            else
            {
                Console.WriteLine($"User with email {email} doesn't exist");
            }
        }
    }
}