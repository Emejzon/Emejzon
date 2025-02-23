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
            var DB = DBManager.Instance();
            if (DB.IsConnect())
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

                using var select = new MySqlCommand("SELECT Email, PhoneNumber FROM users;", DB.Conn);
                using var reader = select.ExecuteReader();

                if (reader.HasRows)
                {
                    using var insert = new MySqlCommand($"INSERT INTO users(Name,Surname,PhoneNumber,City,Address,Email,PASSWORD,Position) VALUES (\"{name} \",\"{surname}\",\"{num}\",\"{city}\",\"{address}\",\"{email}\",\"{password}\",\"{position}\")", DB.Conn);
                    insert.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("User with same email or phone number already exist!");
                }
            }
            else
            {
                Console.WriteLine("Database connection error");
            }

        }
        static void ModifyUser()
        {
            throw new NotImplementedException();
        }
        static void DeleteUser()
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                Console.WriteLine("Insert email of user that you want to delete: ");
                string? email = Console.ReadLine();

                using var select = new MySqlCommand($"Select Email from users where email = \"{email}\" ", DB.Conn);
                using var reader = select.ExecuteReader();

                if (reader.HasRows)
                {
                    using var delete = new MySqlCommand($"DELETE FROM users WHERE Email = \"{email}\" ", DB.Conn);
                    delete.ExecuteNonQuery();
                    Console.WriteLine($"User with email {email} removed");
                }
                else
                {
                    Console.WriteLine($"User with email {email} doesn't exist");
                }
            }
            else
            {
                Console.WriteLine("Database connection error");
            }
        }
    }
}