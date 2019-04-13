// ***********************************************************************
// Author     ：HuYe
// Function   ：Socket基类
// CreateTime ：2019/4/13 9:50:22
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketService
{
    public abstract class TSocketBase
    {
        //封装socket
        public Socket _Socket;
        //回调
        private AsyncCallback Callback;
        //接受数据的缓冲区
        private byte[] Buffer;
        //标识是否已经释放
        private volatile bool IsDispose;
        //10K的缓冲区空间
        private int BufferSize = 10 * 1024;
        //收取消息状态码
        private SocketError ReceiveError;
        //发送消息的状态码
        private SocketError SendError;
        //每一次接受到的字节数
        private int ReceiveSize = 0;
        //接受空消息次数
        private byte ZeroCount = 0;
        //消息解析器
        private ProtocolHelper PHelper = new ProtocolHelper();

        public abstract void ReceiveMsg(TSocketMessage msg);

        /// <summary>
        /// 设置通信Socket
        /// </summary>
        public void SetSocket()
        {
            this.Callback = new AsyncCallback(ReceiveCallback);
            this.IsDispose = false;
            this._Socket.ReceiveBufferSize = this.BufferSize;
            this._Socket.SendBufferSize = this.BufferSize;
            this.Buffer = new byte[this.BufferSize];
        }

        private void ReceiveCallback(IAsyncResult iar)
        {
            if (!this.IsDispose)
            {
                try
                {
                    // 接收消息
                    ReceiveSize = _Socket.EndReceive(iar, out ReceiveError);
                    // 检查状态码
                    if(!CheckSocketError(ReceiveError) && SocketError.Success == ReceiveError)
                    {
                        if (ReceiveSize > 0)
                        {
                            byte[] rbuff = new byte[ReceiveSize];
                            Array.Copy(this.Buffer, rbuff, ReceiveSize);
                            var msgs = PHelper.UnpackData(rbuff, ReceiveSize);
                            foreach(var msg in msgs)
                            {
                                this.ReceiveMsg(msg);
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// 递归接收消息
        /// </summary>
        internal void ReceiveAsync()
        {
            try
            {
                if(!this.IsDispose && this._Socket.Connected)
                {
                    this._Socket.BeginReceive(this.Buffer, 0, this.BufferSize, SocketFlags.None, out ReceiveError, Callback, this);
                    CheckSocketError(ReceiveError);
                }
            }
            catch (SocketException)
            {
                this.Close("链接已经被关闭!");
            }
            catch (ObjectDisposedException)
            {
                this.Close("链接已经被关闭!");
            }
        }

        /// <summary>
        /// 关闭Socket
        /// </summary>
        /// <param name="msg"></param>
        public void Close(string msg)
        {
            if (!this.IsDispose)
            {
                this.IsDispose = true;
                try
                {
                    try { this._Socket.Close(); }
                    catch { }
                    IDisposable disposable = this._Socket;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                    this.Buffer = null;
                    GC.SuppressFinalize(this);
                }
                catch { }
            }
        }

        /// <summary>
        /// 错误检查
        /// </summary>
        /// <param name="socketError"></param>
        /// <returns></returns>
        private bool CheckSocketError(SocketError socketError)
        {
            switch (socketError)
            {
                case SocketError.SocketError:
                case SocketError.VersionNotSupported:
                case SocketError.TryAgain:
                case SocketError.ProtocolFamilyNotSupported:
                case SocketError.ConnectionAborted:
                case SocketError.ConnectionRefused:
                case SocketError.ConnectionReset:
                case SocketError.Disconnecting:
                case SocketError.HostDown:
                case SocketError.HostNotFound:
                case SocketError.HostUnreachable:
                case SocketError.NetworkDown:
                case SocketError.NetworkReset:
                case SocketError.NetworkUnreachable:
                case SocketError.NoData:
                case SocketError.OperationAborted:
                case SocketError.Shutdown:
                case SocketError.SystemNotReady:
                case SocketError.TooManyOpenSockets:
                    this.Close(socketError.ToString());
                    return true;
            }
            return false;
        }
    }
}
