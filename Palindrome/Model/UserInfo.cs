using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">用户密码</param>
        public UserInfo(string userName, string userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
        }

        public string UserName { get; set; }         //用户名
        public string UserPassword { get; set; }     //用户密码
    }
}
