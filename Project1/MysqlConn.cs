using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Project1;
namespace test
{
    class MysqlConn
    {
        public static MySqlConnection Connection()
        {
            string strConn = "server = localhost; User Id = root; password = root; Database = ty; Charset = utf8";
            MySqlConnection conn = new MySqlConnection(strConn);
            return conn;
        }
        //static void Main(string[] args)
        //{
        //    int cnt = 0;
        //    while (cnt++ < 10)
        //    {


        //        RSAKEY kkk = RSAByPrivate.GetKey();
        //        Console.WriteLine(kkk.PublicKey + '\n');
        //    }
        //    //MySqlConnection conn = Connection();
        //    //conn.Open();
        //    //string sql = "select * from users";
        //    //MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    //MySqlDataReader reader = cmd.ExecuteReader();
        //    //while (reader.Read())
        //    //{
        //    //    Console.WriteLine(reader.GetString("useID"));

        //    //}
        //    Console.ReadKey(true);
        //}
    }
}
