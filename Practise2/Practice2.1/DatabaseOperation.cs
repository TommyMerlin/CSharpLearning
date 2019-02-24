using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Practice2._1
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class DatabaseOperation
    {
        /// <summary>
        /// 返回连接字符串
        /// </summary>
        public  string Connstr
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["MyConStr"].ConnectionString.ToString();
            }
        }
    }
}
