using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * Database Connection here
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinder
{
    public class DBConnection
    {
        private DBConnection _connection;



        private DBConnection(string Server, string Database, string UserName, string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.Username = UserName;
            this.Password = Password;
        }



        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }



        public MySqlConnection Connection { get; set; }



        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            // Settings here for DB connection

            string Server = "localhost";
            string Database = "filmfinder";
            string Username = "root";
            string Password = "";



            if (_instance == null)
                _instance = new DBConnection(Server, Database, Username, Password);
            return _instance;
        }
        public static DBConnection Instance(string Server, string DatabaseName, string UserName, string Password)
        {
            _instance = new DBConnection(Server, DatabaseName, UserName, Password);
            return _instance;
        }



        public bool IsConnected()
        {
            if (Connection == null)
            {
                if (string.IsNullOrEmpty(Database))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, Database, Username, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }



            return true;
        }



        public void Close()
        {
            Connection.Close();
        }


    }
}