using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emejzon.Services;
using Emejzon.Users;
using MySqlConnector;

namespace Emejzon.Interfaces
{
    internal interface ISystemManagement
    {
        static void AddUser(int userId)
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
                string pos = Console.ReadLine();
                pos = pos.ToLower();
                switch (pos)
                {
                    case "admin":
                        pos = "Admin";
                        break;
                    case "manager":
                        pos = "Manager";
                        break;
                    case "worker":
                        pos = "Worker";
                        break;
                    default:
                        pos = "Client";
                        break;
                }

                using var select = new MySqlCommand($"SELECT Email, PhoneNumber FROM users where(Email = \"{email}\" or PhoneNumber = {num});", DB.Conn);
                using var reader = select.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.DisposeAsync();
                    using var insert = new MySqlCommand($"INSERT INTO users(Name,Surname,PhoneNumber,City,Address,Email,Password,Position) VALUES (\"{name} \",\"{surname}\",\"{num}\",\"{city}\",\"{address}\",\"{email}\",\"{password}\",\"{pos}\")", DB.Conn);
                    insert.ExecuteNonQuery();
                    Console.WriteLine($"User with email {email} added");
                    LogManager.AddLogEntry(userId, $"User with email {email} added");
                }
                else
                {
                    Console.WriteLine("User with same email or phone number already exist!");
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
        static void ModifyUser(int userId)
        {
            Console.WriteLine("Insert id: ");
            int id = int.Parse(Console.ReadLine());
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var select = new MySqlCommand($"SELECT * FROM users where id = {id}", DB.Conn);
                using var reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.DisposeAsync();

                    Console.Write("Select option: ");
                    var choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("[1]Name");
                    Console.WriteLine("[2]Surname");
                    Console.WriteLine("[3]Email");
                    Console.WriteLine("[4]Phone number");
                    Console.WriteLine("[5]City");
                    Console.WriteLine("[6]Address");
                    Console.WriteLine("[7]Position");
                    Console.WriteLine("[8]Password");
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Insert new name:");
                            string name = Console.ReadLine();
                            {
                                using var update = new MySqlCommand($"UPDATE users SET Name = \"{name}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        case 2:
                            Console.WriteLine("Insert new surname:");
                            string surname = Console.ReadLine();
                            {
                                using var update = new MySqlCommand($"UPDATE users SET Surname = \"{surname}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        case 3:
                            Console.WriteLine("Insert new email:");
                            string emailAddress = Console.ReadLine();
                            {
                                using var email = new MySqlCommand($"SELECT PhoneNumber FROM users where PhoneNumber = {emailAddress}", DB.Conn);
                                using var checkEmail = email.ExecuteReader();
                                if (!checkEmail.HasRows)
                                {
                                    using var update = new MySqlCommand($"UPDATE users SET Email = {emailAddress} WHERE id = {id}", DB.Conn);
                                    update.ExecuteNonQuery();
                                }
                                else
                                {
                                    Console.WriteLine("User with this email already exist");
                                }
                                checkEmail.DisposeAsync();
                            }
                            break;
                        case 4:
                            Console.WriteLine("Insert new phone number:");
                            int num = int.Parse(Console.ReadLine());
                            {
                                using var phoneNum = new MySqlCommand($"SELECT PhoneNumber FROM users where PhoneNumber = {num}", DB.Conn);
                                using var checkPhoneNum = phoneNum.ExecuteReader();
                                if (!checkPhoneNum.HasRows)
                                {
                                    using var update = new MySqlCommand($"UPDATE users SET PhoneNumber = {num} WHERE id = {id}", DB.Conn);
                                    update.ExecuteNonQuery();
                                }
                                checkPhoneNum.DisposeAsync();
                            }
                            break;
                        case 5:
                            Console.WriteLine("Insert new city:");
                            string city = Console.ReadLine();
                            {
                                using var update = new MySqlCommand($"UPDATE users SET City = \"{city}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        case 6:
                            Console.WriteLine("Insert new address:");
                            string address = Console.ReadLine();
                            {
                                using var update = new MySqlCommand($"UPDATE users SET Address = \"{address}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        case 7:
                            Console.WriteLine("Insert new position:");
                            string pos = Console.ReadLine();
                            {
                                pos = pos.ToLower();
                                switch (pos)
                                {
                                    case "admin":
                                        pos = "Admin";
                                        break;
                                    case "manager":
                                        pos = "Manager";
                                        break;
                                    case "worker":
                                        pos = "Worker";
                                        break;
                                    default:
                                        pos = "Client";
                                        break;
                                }
                                using var update = new MySqlCommand($"UPDATE users SET Position = \"{pos}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        case 8:
                            Console.WriteLine("Insert new password:");
                            string password = Console.ReadLine();
                            password = PasswordManager.HashPassword(password);
                            {
                                using var update = new MySqlCommand($"UPDATE users SET Password = \"{password}\" WHERE id = {id}", DB.Conn);
                                update.ExecuteNonQuery();
                            }
                            break;
                        default:
                            Console.WriteLine("Choose a valid option");
                            break;
                    }
                    Console.WriteLine($"User with id {id} modified");
                    LogManager.AddLogEntry(userId, $"User with id {id} modified");
                }
                else
                {
                    Console.WriteLine("User not found");
                }
            }
            else
            {
                Console.WriteLine("Database connection error");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void DeleteUser(int userId)
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
                    reader.DisposeAsync();
                    using var delete = new MySqlCommand($"DELETE FROM users WHERE Email = \"{email}\" ", DB.Conn);
                    delete.ExecuteNonQuery();
                    Console.WriteLine($"User with email {email} removed");
                    LogManager.AddLogEntry(userId, $"User with email {email} removed");
                }
                else
                {
                    Console.WriteLine($"User with email {email} doesn't exist");
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
        static void ShowLogs()
        {
            throw new NotImplementedException();
        }
    }
}