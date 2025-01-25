using System.Security.Cryptography;
using System.Text;
using MySqlConnector;
namespace Emejzon.Login
{
    public class PasswordManager
    {
        private const string Email;
        private const string Password;
        public static Action<string, bool> PasswordVerify;
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public static bool VerifyPassword(string email, string password)
        {
            string hashPassword = HashPassword(password);
            using var connection = new MySqlConnection("server=127.0.0.1;user=root;database=Emejzon;password=admin123");

            connection.Open();
            using var command = new MySqlCommand("SELECT Email, Password FROM users;", connection);
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
            return false;
        }
    }
}