using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
{
    /// <summary>
    /// 底层通信消息
    /// </summary>
    public class TSocketMessage : IDisposable
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public byte[] MsgBuffer;

        /// <summary>
        /// 消息类型
        /// </summary>
        public byte MsgType;

        #region Constructor

        public TSocketMessage(byte[] msg, byte msgType)
        {
            this.MsgBuffer = msg;
            MsgType = msgType;
        }

        public TSocketMessage(byte[] msg)
        {
            this.MsgBuffer = msg;
        }

        public TSocketMessage(StoreHeap heap)
        {
            using (MMO_MemoryStream ms = new MMO_MemoryStream())
            {
                foreach (Node node in heap)
                {
                    ms.WriteShort((short)node.Index);
                    ms.WriteShort((short)node.X);
                    ms.WriteShort((short)node.Y);
                    ms.WriteShort((short)node.Direction);
                }
                MsgBuffer = ms.ToArray();
            }
        }

        #endregion

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool flag1)
        {
            if (flag1) { this.MsgBuffer = null; }
        }
    }
}
