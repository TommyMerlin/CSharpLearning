using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BLL
{
    public class LoginManager
    {
        public bool LoginComfirm(Model.UserInfo userInfo)
        {
            string sql = "select * from tbUser where UserName = @userName and UserPwd = @userPwd";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@userName",userInfo.UserName),
                new SqlParameter("@userPwd",userInfo.UserPwd)
            };
            try
            {
                int count = DAL.SQLHelper.ExecuteSQLReturnInt(sql, pars);
                if(count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
