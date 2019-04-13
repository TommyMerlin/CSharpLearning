// ***********************************************************************
// Author     ：HuYe
// Function   ：Socket 通信辅助类
// CreateTime ：2019/4/11 15:39:30
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Controls;

namespace SocketService
{
    class SocketHelper
    {
        public void ReceiveMsg(object clientSocket,TextBox textBox)
        {
            Socket connection = (Socket)clientSocket;
            byte[] partialBuffer = null;
            while (true)
            {

                try
                {
                    byte[] result = new byte[2048];

                    ////通过clientSocket接收数据  
                    //int receiveNumber = connection.Receive(result);
                    ////把接受的数据从字节类型转化为字符类型
                    //string recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);


                    int receiveNumber = connection.Receive(result);
                    byte[] buffer = new byte[receiveNumber];
                    Buffer.BlockCopy(result, 0, buffer, 0, receiveNumber);

                    if (partialBuffer != null)
                    {
                        receiveNumber += partialBuffer.Length;
                        buffer = partialBuffer.Concat(buffer).ToArray();
                    }

                    ProtocolHelper.MMO_MemoryStream ms = new ProtocolHelper.MMO_MemoryStream(buffer);
                    int length = ms.ReadUShort();
                    ms.Close();

                    if (length < buffer.Length)
                    {
                        partialBuffer = buffer;
                        continue;
                    }


                    partialBuffer = null;
                    byte[] unpackedMsg = ProtocolHelper.UnpackData(buffer);
                    string recStr = Encoding.Unicode.GetString(unpackedMsg, 0, unpackedMsg.Length);



                    //获取当前客户端的ip地址
                    IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                    //获取客户端端口
                    int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                    string sendStr = clientIP + ":" + clientPort.ToString() + "--->" + recStr;
                    //显示内容
                    textBox.Dispatcher.BeginInvoke(

                            new Action(() => { textBox.Text = $"【接收信息】 {sendStr}\r\n" + textBox.Text; }), null);

                }
                catch (Exception ex)
                {
                    // 出现异常，关闭连接
                    connection.Shutdown(SocketShutdown.Both);
                    connection.Close();
                    textBox.Dispatcher.BeginInvoke(

                            new Action(() => { textBox.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + textBox.Text; }), null);
                    break;
                }
            }
        }
    }
}
