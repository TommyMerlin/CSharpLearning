using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        public UserInfo(string userName, string userPwd)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            UserPwd = userPwd ?? throw new ArgumentNullException(nameof(userPwd));
        }

        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}
