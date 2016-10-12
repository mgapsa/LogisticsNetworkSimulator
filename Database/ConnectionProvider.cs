using Database.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Database
{
    public class ConnectionProvider
    {
        private string url = "Server=" + Settings.Default.DbServer + ";Database=" +
            Settings.Default.DbName + ";Uid=" + Settings.Default.DbUsername + ";Pwd=" + Settings.Default.DbPwd + ";";
        //       private string url = "Server=" + "localhost" + ";Database=" +
        //"fhv" + ";Uid=" + "root" + ";Pwd=" + "admin" + ";";
        public IDbConnection GetConnection(bool open)
        {
            IDbConnection conn = new MySqlConnection(url);
            if (open)
            {
                conn.Open();
                return conn;
            }
            else
            {
                return conn;
            }
        }
    }
}
