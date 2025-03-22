using MySqlConnector;

namespace Emejzon.Services
{
    public delegate void LogHandler(int userId, string message);
    public interface ILogger
    {
        void AddLogEntry(int userId, string message);
    }
    public class AddLocalLog : ILogger
    {
        public void AddLogEntry(int userId, string message)
        {
            string logEntry = $"[{DateTime.Now:dd-MM-yyyy|HH-mm}]User: {userId} - {message}\n";
            File.AppendAllText("log.txt", logEntry);
        }
    }
    public class AddDatabaseLog : ILogger
    {
        public void AddLogEntry(int userId, string message)
        {
            var DB = DBManager.Instance();
            if (DB.IsConnect())
            {
                using var insert = new MySqlCommand($"Insert into log(UserId,Message) values ({userId},\"{message}\");", DB.Conn);
                insert.ExecuteNonQuery();
                DB.Close();
            }
            else
            {
                Console.WriteLine("Database connection error!");
            }
        }
    }
    public class LogManager
    {
        public LogHandler Log;
        public void SendLogEntry(int userId, string message)
        {
            foreach (var handler in Log.GetInvocationList())
            {
                handler.DynamicInvoke(userId, message);
            }
        }
        public static void AddLogEntry(int userId, string message)
        {
            LogManager logManager = new LogManager();
            if (logManager.Log == null)
            {
                logManager.Log += new AddLocalLog().AddLogEntry;
                logManager.Log += new AddDatabaseLog().AddLogEntry;
            }
            
            logManager.SendLogEntry(userId, message);
        }
    }
}