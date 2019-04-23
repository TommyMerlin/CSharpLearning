using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketService
{
    public abstract class TSocketBase
    {
        #region Field
        //封装socket
        public Socket _Socket;
        //接受数据的缓冲区
        private byte[] Buffers;
        //标识是否已经释放
        private volatile bool IsDispose;
        //10K的缓冲区空间
        private readonly int BufferSize = 10 * 1024;
        //每一次接受到的字节数
        private int ReceiveSize = 0;
        // 消息解析器
        private ProtocolHelper PHelper = new ProtocolHelper();
        #endregion

        /// <summary>
        /// 对接收的信息进行处理
        /// </summary>
        /// <param name="msg">信息</param>
        public abstract void Receive(TSocketMessage msg);

        /// <summary>
        /// 设置Socket参数
        /// </summary>
        public void SetSocket()
        {
            this.IsDispose = false;
            this._Socket.ReceiveBufferSize = this.BufferSize;
            this._Socket.SendBufferSize = this.BufferSize;
            this.Buffers = new byte[this.BufferSize];
        }


        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        /// <param name="msg"></param>
        public void Close(string msg)
        {
            if (!this.IsDispose)
            {
                this.IsDispose = true;
                try
                {
                    this._Socket.Close();
                    IDisposable disposable = this._Socket;
                    if (disposable != null) { disposable.Dispose(); }
                    this.Buffers = null;
                    GC.SuppressFinalize(this);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        public void Close()
        {
            if (!this.IsDispose)
            {
                this.IsDispose = true;
                try
                {
                    try { this._Socket.Close(); }
                    catch { }
                    IDisposable disposable = this._Socket;
                    if (disposable != null) { disposable.Dispose(); }
                    this.Buffers = null;
                    GC.SuppressFinalize(this);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="iar"></param>
        public void ReceiveMsg()
        {
            while (true)
            {
                try
                {
                    if (!this.IsDispose && this._Socket.Connected)
                    {
                        ReceiveSize = this._Socket.Receive(this.Buffers);
                        if (ReceiveSize > 0)
                        {
                            byte[] rbuff = new byte[ReceiveSize];
                            Array.Copy(this.Buffers, rbuff, ReceiveSize);
                            var msgs = PHelper.UnpackData(rbuff, ReceiveSize);
                            foreach (var msg in msgs)
                            {
                                this.Receive(msg);
                            }
                        }
                    }
                }
                catch
                {
                    this.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 发送消息方法
        /// </summary>
        public int SendMsg(TSocketMessage msg, MessageType msgType)
        {
            int size = 0;
            try
            {
                if (!this.IsDispose)
                {
                    //封包
                    byte[] buffer = PHelper.PackData(msg, msgType);
                    size = this._Socket.Send(buffer);
                }
            }
            catch (System.ObjectDisposedException) { this.Close("链接已经被关闭"); }
            catch (System.Net.Sockets.SocketException) { this.Close("链接已经被关闭"); }
            msg.Dispose();
            return size;
        }
    }
}