using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Palindrome
{
    class SQLServer
    {
        private static SqlConnection conn = null;
        private static SqlCommand cmd = null;

        static void Main(string[] args)
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            conn = new SqlConnection(conStr);
            conn.Open();
            string insert = "insert into node(id,x,y)values(23,23,23)";
            cmd = new SqlCommand(insert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Data inserted successfully!");

            Console.Read();
        }
    }
}
