using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace test
{
    class MysqlConn
    {
        public MySqlConnection Connection()
        {
            string strConn = "server = localhost; User Id = root; password = root; Database = bs; Charset = utf8";
            MySqlConnection conn = new MySqlConnection(strConn);
            return conn;
        }
    }
}
