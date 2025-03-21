using MySqlConnector;
using System.IO;

namespace Emejzon.Services
{
    public class DBManager
    {
        private DBManager() { }
        public MySqlConnection Conn { get; private set; }
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
            }

            if (Conn.State == System.Data.ConnectionState.Closed || Conn.State == System.Data.ConnectionState.Broken)
            {
                Conn.Open();
            }

            return Conn.State == System.Data.ConnectionState.Open;
        }

        public void Close()
        {
            if (Conn != null && Conn.State == System.Data.ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
}