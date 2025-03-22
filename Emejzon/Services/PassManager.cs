using System.Security.Cryptography;
using System.Text;
using MySqlConnector;
using Emejzon.Services;

namespace Emejzon.Services
{
    public class PasswordManager
    {
        public static event Action<string, bool> PasswordVerify;
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public static bool VerifyPassword(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            var DB = DBManager.Instance();

            if (DB.IsConnect())
            {
                using var command = new MySqlCommand("SELECT Email, Password FROM users;", DB.Conn);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(0) == email && reader.GetString(1) == hashedPassword)
                    {
                        PasswordVerify?.Invoke(email, true);
                        return true;
                    }
                }
                PasswordVerify?.Invoke(email, false);
                DB.Close();
                return false;
            }
            else
            {
                Console.WriteLine("Database connection error");
                return false;
            }
        }
    }
}