// ***********************************************************************
// Author     ：HuYe
// Function   ：底层通讯信息
// CreateTime ：2019/4/13 9:58:16
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
{
    /// <summary>
    /// 底层通讯信息类
    /// </summary>
    public class TSocketMessage : IDisposable
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MsgID;
        /// <summary>
        /// 消息内容
        /// </summary>
        public byte[] MsgBuffer;

        public TSocketMessage(int msgID, byte[] msg)
        {
            this.MsgID = msgID;
            this.MsgBuffer = msg;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool flag)
        {
            if (flag)
                this.MsgBuffer = null;
        }
    }
}
