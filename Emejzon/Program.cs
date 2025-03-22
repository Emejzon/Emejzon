using System.Data.Common;
using Emejzon.Services;
using Emejzon.Users;
using MySqlConnector;

namespace Emejzon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PasswordManager.PasswordVerify += (email, success) => Console.WriteLine
            ($"Login for {email} {(success ? "succeded" : "failed")}");
            while (true)
            {
                try
                {
                    ShowMenu();
                    var choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Login();
                            break;
                        case 2:
                            CreateClientAccount();
                            break;
                        case 9:
                            Console.WriteLine("See you next time");
                            return;
                        default:
                            Console.WriteLine("Chose different option");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("[1]. Login");
            Console.WriteLine("[2]. Create client account");
            Console.WriteLine("[9]. Exit program");
            Console.Write("Select option: ");
        }
        public static void Login()
        {
            Console.WriteLine("Insert email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Insert password: ");
            string? password = Console.ReadLine();
            
            if (PasswordManager.VerifyPassword(email, password))
            {
                var DB = DBManager.Instance();
                if(DB.IsConnect())
                {
                    using var select = new MySqlCommand($"Select * from users where email = \"{email}\"",DB.Conn);
                    using var reader = select.ExecuteReader();
                    reader.Read();

                    if(reader.GetString(7) == "Client")
                    {
                        Client client = new Client(reader.GetInt32(0),reader.GetString(1),Position.Client);
                        DB.Close();
                        while (true)
                        {
                            client.ClientMenu(client.Id);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        string pos = reader.GetString(7);
                        Role role;
                        switch (pos)
                        {
                            case "Admin":
                                role = Role.Admin;
                                break;
                            case "Manager":
                                role = Role.Manager;
                                break;
                            case "Worker":
                                role = Role.Worker;
                                break;
                            default:
                                role = Role.Worker;
                                break;
                        }

                        Worker worker = new Worker(reader.GetInt32(0),reader.GetString(1),Position.Worker,role);
                        DB.Close();
                        while (true)
                        {
                            worker.WorkersMenu(worker.Role, worker.Id);
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Database connection error");
                }
            }
            else
            {
                Console.WriteLine("Email or password incorrect");
                Console.ReadKey();
            }
        }
        public static void CreateClientAccount()
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

                using var command = new MySqlCommand("SELECT Email, PhoneNumber FROM users;", DB.Conn);
                using var reader = command.ExecuteReader();

                bool exist = false;

                while (reader.Read())
                {
                    if (reader.GetString(0) == email || reader.GetInt64(1) == num)
                    {
                        exist = true;
                        Console.WriteLine("User with this email or phone number already exist");
                    }
                }
                reader.Dispose();
                if (!exist)
                {
                    using var newUser = new MySqlCommand($"INSERT INTO users(Name,Surname,PhoneNumber,City,Address,Email,PASSWORD) VALUES (\"{name} \",\"{surname}\",\"{num}\",\"{city}\",\"{address}\",\"{email}\",\"{password}\")", DB.Conn);
                    newUser.ExecuteNonQuery();
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
