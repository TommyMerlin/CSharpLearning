using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class UserDB
    {
        private string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();

        public void AddUser(Model.UserInfo userInfo)
        {
            userInfo.UserName = "huye";
            userInfo.UserPassword = "123456";
            Console.WriteLine($"UserName: {userInfo.UserName}\nUserPassword: {userInfo.UserPassword}");
        }
    }
}
