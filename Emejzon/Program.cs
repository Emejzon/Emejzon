using Emejzon.Services;
using MySqlConnector;

namespace Emejzon
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            PasswordManager.PasswordVerify += (email, success) => Console.WriteLine
            ($"Login for {email} {(success ? "succeded" : "failed")}");

            if (PasswordManager.VerifyPassword(email, password))
            {
                Console.ReadKey();
                //create new client or worker instance and trigger proper menu
            }
            else
            {
                Console.WriteLine("Email or password incorrect");
                Console.ReadKey();
            }
        }
        public static void CreateClientAccount()
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

            using var connection = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            connection.Open();

            using var command = new MySqlCommand("SELECT Email, PhoneNumber FROM users;", connection);
            using var reader = command.ExecuteReader();

            bool exist = false;

            while (reader.Read())
            {
                if(reader.GetString(0) == email || reader.GetInt64(1) == num){
                    exist = true;
                }
            }
            connection.Dispose();

            using var conn = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");
            conn.Open();

            if(!exist)
            {
                using var newUser = new MySqlCommand($"INSERT INTO users(Name,Surname,PhoneNumber,City,Address,Email,PASSWORD) VALUES (\"{name} \",\"{surname}\",\"{num}\",\"{city}\",\"{address}\",\"{email}\",\"{password}\")", conn);
                newUser.ExecuteNonQuery();
            }
        }
    }
}
