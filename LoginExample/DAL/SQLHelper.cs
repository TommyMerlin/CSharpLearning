using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 数据库连接和操作
    /// </summary>
    public class SQLHelper
    {
        #region Properties

        private static string ConnStr
        {
            get
            {
                return "Data Source = LAPTOP-GQ245MB8; Initial Catalog = agv; User ID = sa; Pwd = zjuhuye";
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 执行SQL语句,返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL命令文本</param>
        /// <created>HuYe,2019/3/1</created>
        /// <changed>HuYe,2019/3/1</changed>
        public static int ExecuteSQLReturnInt(string cmdText)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);        //新建命令对象

                //打开连接对象
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch
            {
                throw new Exception("数据查询失败");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行带参数的SQL语句,返回受影响的行数,重载ExecuteSQLReturnInt方法
        /// </summary>
        /// <param name="cmdText">SQL命令文本</param>
        /// <param name="pars">参数列表</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteSQLReturnInt(string cmdText, SqlParameter[] pars)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象

            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);        //新建命令对象

                //打开连接对象
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }

                if (pars != null && pars.Length > 0)
                {
                    foreach (SqlParameter p in pars)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch
            {
                throw new Exception("数据查询失败");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 实现SQL数据查询，返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>数据集合</returns>
        public DataSet SelectSQLReturnDataset(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);        //新建数据适配器对象

            DataSet dataSet = new DataSet();
            sda.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 实现基于带参数的SQL查询,重载SelectSQLReturnDataset方法
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="pars">SQL参数</param>
        /// <returns>DataSet数据集合</returns>
        public DataSet SelectSQLReturnDataset(string sql,SqlParameter[] pars)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);        //新建数据适配器对象

            if (pars != null && pars.Length > 0)
            {
                foreach(SqlParameter par in pars)
                {
                    sda.SelectCommand.Parameters.Add(par);
                }
            }

            DataSet dataSet = new DataSet();
            sda.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 实现SQL数据查询，返回数据表
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        public DataTable SelectSQLReturnDataTable(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);        //新建数据适配器对象
            DataSet ds = new DataSet();
            sda.Fill(ds, "MyTable");
            return ds.Tables["MyTable"];
        }
        
        /// <summary>
        /// 实现SQL数据查询，返回数据表，重载SelectSQLReturnDataTable方法
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        public DataTable SelectSQLReturnDataTable(string sql, SqlParameter[] pars, CommandType type)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);        //新建数据适配器对象

            if(pars != null && pars.Length > 0)
            {
                foreach(SqlParameter par in pars)
                {
                    sda.SelectCommand.Parameters.Add(par);
                }
            }

            sda.SelectCommand.CommandType = type;
            DataSet ds = new DataSet();
            sda.Fill(ds, "MyTable");
            return ds.Tables["MyTable"];
        }

        /// <summary>
        /// 实现SQL查询
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>DataReader数据阅读器</returns>
        public SqlDataReader SelectSQLReturnReader(string sql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
                SqlCommand cmd = new SqlCommand(sql, conn);                //新建命令对象

                //打开连接对象
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                throw new Exception("数据查询失败");
            }
        }

        /// <summary>
        /// 实现数据库查询,基于SQL命令或存储过程，重载SelectSQLReturnReader方法
        /// </summary>
        /// <param name="sql">"select * from node where id = @id"</param>
        /// <param name="type">命令类型</param>
        /// <param name="pars">参数</param>
        /// <returns>DataReader数据阅读器</returns>
        public SqlDataReader SelectSQLReturnReader(string sql, CommandType type, SqlParameter[] pars)
        {
            SqlConnection conn = new SqlConnection(ConnStr);           //新建连接对象
            SqlCommand cmd = new SqlCommand(sql, conn);                //新建命令对象

            if(pars != null && pars.Length > 0)                        //添加参数
            {
                foreach(SqlParameter par in pars)
                {
                    cmd.Parameters.Add(par);
                }
            }

            cmd.CommandType = type;                                    //指定命令类型
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        #endregion
    }
}
