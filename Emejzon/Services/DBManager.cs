using MySqlConnector;
namespace Emejzon.Services
{
    public class DBManager
    {
        private DBManager(){}
        public MySqlConnection Conn { get; set; }
        private static DBManager _instance = null;
        public static DBManager Instance()
        {
            if (_instance == null)
            {
                _instance = new DBManager();
            }
            return _instance;
        }
        public bool IsConnect()
        {
            if (Conn == null)
            {
                string connstring = File.ReadAllText("database.txt");
                Conn = new MySqlConnection(connstring);
                Conn.Open();
            }
    
            return true;
        }
    }
}