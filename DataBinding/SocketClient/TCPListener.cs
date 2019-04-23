using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SocketService
{
    /// <summary>
    /// 建立TCP通信监听服务
    /// </summary>
    public class TCPListener
    {
        private IPEndPoint _IP;
        private Socket _Listeners;
        //标识是否为初始化状态
        private volatile bool IsInit = false;
        List<TSocketBase> sockets = new List<TSocketBase>();
        //接受数据的缓冲区
        private byte[] Buffers;
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
        //通讯协议解析
        private ProtocolHelper PHelper = new ProtocolHelper();
        //回调
        

        /// <summary>
        /// 初始化服务器
        /// </summary>
        public TCPListener(string ip = "127.0.0.1", int port = 9500)
        {
            IsInit = true;
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);
            this._IP = localEP;
            try
            {
                this._Listeners = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SetSocket();
                this._Listeners.Bind(this._IP);
                this._Listeners.Listen(5000);
                Thread th = new Thread(ListenClientConnect)
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch
            {
                this.Dispose();
                IsDispose = true;
                throw;
            }
        }

        public void SetSocket()
        {
            this.IsDispose = false;
            this._Listeners.ReceiveBufferSize = this.BufferSize;
            this._Listeners.SendBufferSize = this.BufferSize;
            this.Buffers = new byte[this.BufferSize];
        }

        public void ListenClientConnect()
        {
            while (true)
            {
                TSocketBase clientSocket = new TSocketClient(_Listeners.Accept());    //连接到服务端的客户端Sokcet
                // 客户端字典中添加连接上的客户端
                sockets.Add(clientSocket);

                //// 新开线程时刻更新客户端列表
                //Thread appendIpThread = new Thread(RefreshIpList)
                //{
                //    IsBackground = true
                //};
                //appendIpThread.Start();

                // 新开线程监听连接上的客户端的信息传输
                Thread receivedThread = new Thread(ReceiveMsg)
                {
                    IsBackground = true
                };
                receivedThread.Start(clientSocket);
            }
        }

        public void ReceiveMsg(object clientSocket)
        {
            TSocketClient connection = (TSocketClient)clientSocket;
            while (true)
            {
                try
                {
                    if (!this.IsDispose && connection._Socket.Connected)
                    {
                        ReceiveSize = connection._Socket.Receive(this.Buffers);
                        if (ReceiveSize > 0)
                        {
                            byte[] rbuff = new byte[ReceiveSize];
                            Array.Copy(this.Buffers, rbuff, ReceiveSize);
                            var msgs = PHelper.UnpackData(rbuff, ReceiveSize);
                            foreach (var msg in msgs)
                            {
                                connection.Receive(msg);
                            }
                        }
                    }
                }
                catch
                {
                    connection.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (IsInit)
            {
                IsInit = false;
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// 释放所占用的资源
        /// </summary>
        /// <param name="flag1"></param>
        protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool flag1)
        {
            if (flag1)
            {
                if (_Listeners != null)
                {
                    try
                    {
                        Console.WriteLine(string.Format("Stop Listener Tcp -> {0}:{1} ", this.IP.Address.ToString(), this.IP.Port));
                        _Listeners.Close();
                        _Listeners.Dispose();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 获取绑定终结点
        /// </summary>
        public IPEndPoint IP { get { return this._IP; } }

        /// <summary>
        /// 错误判断
        /// </summary>
        /// <param name="socketError"></param>
        /// <returns></returns>
        bool CheckSocketError(SocketError socketError)
        {
            switch ((socketError))
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
                    return true;
            }
            return false;
        }
    }
}
