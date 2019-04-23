using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SocketService
{

    public class TSocketClient : TSocketBase
    {
        /// <summary>
        /// 是否是服务器端的资源
        /// </summary>
        bool isServer = false;

        /// <summary>
        /// 客户端主动请求服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public TSocketClient(string ip = "127.0.0.1", int port = 9500)
        {
            isServer = false;
            this._Socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._Socket.Connect(ip, port);
            this.SetSocket();
            Thread th = new Thread(ReceiveMsg)
            {
                IsBackground = true
            };
            th.Start();
        }
        /// <summary>
        /// 这个是服务器收到有效链接初始化
        /// </summary>
        /// <param name="socket"></param>
        public TSocketClient(Socket socket)
        {
            isServer = true;
            this._Socket = socket;
            this.SetSocket();
        }



        /// <summary>
        /// 收到消息后
        /// </summary>
        /// <param name="rbuff"></param>
        public override void Receive(TSocketMessage msg)
        {
            switch (msg.MsgType)
            {
                case 0:
                    ReceiveInfo(msg);
                    break;
                case 1:
                    ReceiveCommand(msg);
                    break;
                default:
                    break;
            }

            if (isServer)
            {
                //this.SendMsg(new TSocketMessage(Encoding.Unicode.GetBytes("Holle Client！")));
            }
        }

        /// <summary>
        /// 当消息类型为Information时
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveInfo(TSocketMessage msg)
        {
            StoreHeap finalPath = new StoreHeap();

            using (MMO_MemoryStream ms = new MMO_MemoryStream(msg.MsgBuffer))
            {
                // 当未读取到字节流末尾时,将节点信息取出
                while (ms.Length != ms.Position)
                {
                    // 分别读取节点编号、节点X坐标、节点Y坐标、途径节点运动方向
                    Node node = new Node(ms.ReadShort(), ms.ReadShort(), ms.ReadShort(), ms.ReadShort());
                    finalPath.NodeList.Add(node);
                }

            }

            string result = "";

            result += finalPath.NodeList[0].Index.ToString() + "(直行)";

            for (int i = 1; i < finalPath.Count; i++)
            {
                string direction;
                if (finalPath.NodeList[i].Direction == 0)
                {
                    direction = "直行";
                }
                else if (finalPath.NodeList[i].Direction == 1)
                {
                    direction = "左转";
                }
                else if (finalPath.NodeList[i].Direction == 2)
                {
                    direction = "右转";
                }
                else
                {
                    direction = "停止";
                }
                result += " => " + finalPath.NodeList[i].Index.ToString() + $"({direction})";
            }

            MessageBox.Show(result);
        }

        /// <summary>
        /// 当消息类型为Command时
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveCommand(TSocketMessage msg)
        {
            string message = Encoding.Unicode.GetString(msg.MsgBuffer);
            MessageBox.Show("收到命令!");
        }

        public void RecriveClientName(TSocketMessage msg)
        {
            string clientName;
            using (MMO_MemoryStream ms = new MMO_MemoryStream(msg.MsgBuffer))
            {
                clientName = Encoding.Unicode.GetString(ms.ReadBytes((int)ms.Length));
                MessageBox.Show(clientName);
            }
        }
    }
}
