using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Client : Window
    {
        public int Age { get; set; }

        // 客户端Socket
        TSocketClient ClientSocket = null;

        

        public Client()
        {
            InitializeComponent();

        }

        ///// <summary>
        ///// 接收服务端发来的信息
        ///// </summary>
        //public void ReceiveMsg()
        //{
        //    byte[] partialBuffer = null;
        //    while (true)
        //    {

        //        try
        //        {
        //            byte[] result = new byte[2048];

        //            ////通过clientSocket接收数据  
        //            //int receiveNumber = connection.Receive(result);
        //            ////把接受的数据从字节类型转化为字符类型
        //            //string recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);


        //            int receiveNumber = clientSocket.Receive(result);
        //            byte[] buffer = new byte[receiveNumber];
        //            Buffer.BlockCopy(result, 0, buffer, 0, receiveNumber);

        //            if (partialBuffer != null)
        //            {
        //                receiveNumber += partialBuffer.Length;
        //                buffer = partialBuffer.Concat(buffer).ToArray();
        //            }

        //            ProtocolHelper.MMO_MemoryStream ms = new ProtocolHelper.MMO_MemoryStream(buffer);
        //            int length = ms.ReadUShort();
        //            ms.Close();

        //            if (length < buffer.Length)
        //            {
        //                partialBuffer = buffer;
        //                continue;
        //            }


        //            partialBuffer = null;
        //            byte[] unpackedMsg = ProtocolHelper.UnpackData(buffer);
        //            string recStr = Encoding.Unicode.GetString(unpackedMsg, 0, unpackedMsg.Length);



        //            //获取当前客户端的ip地址
        //            IPAddress clientIP = (clientSocket.RemoteEndPoint as IPEndPoint).Address;
        //            //获取客户端端口
        //            int clientPort = (clientSocket.RemoteEndPoint as IPEndPoint).Port;
        //            string sendStr = clientIP + ":" + clientPort.ToString() + "--->" + recStr;
        //            //显示内容
        //            txtboxInfo.Dispatcher.BeginInvoke(

        //                    new Action(() => { txtboxInfo.Text = $"【接收信息】 {sendStr}\r\n" + txtboxInfo.Text; }), null);

        //        }
        //        catch (Exception ex)
        //        {
        //            // 出现异常，关闭连接
        //            clientSocket.Shutdown(SocketShutdown.Both);
        //            clientSocket.Close();
        //            txtboxInfo.Dispatcher.BeginInvoke(

        //                    new Action(() => { txtboxInfo.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + txtboxInfo.Text; }), null);
        //            break;
        //        }
        //    }
        //}

        /// <summary>
        /// 连接服务端
        /// </summary>
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 如果当前已经存在Socket，则先关闭当前Socket
                if (ClientSocket != null)
                {
                    ClientSocket.Close();
                }

                ClientSocket = new TSocketClient(txtboxIP.Text,Convert.ToInt32(txtboxPort.Text));

                txtboxInfo.Text = $"【连接成功】 我方端口 {ClientSocket._Socket.LocalEndPoint.ToString()}\r\n" + txtboxInfo.Text;
                //Thread th = new Thread(ClientSocket.RecvMsg)
                //{
                //    IsBackground = true
                //};
                //th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("【连接失败】 " + ex.Message);
                return;
            }
        }

        ///// <summary>
        ///// 发送信息
        ///// </summary>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string message = txtboxMessage.Text;
            txtboxInfo.Text = $"【发送消息】 {message}\r\n" + txtboxInfo.Text;
            TSocketMessage msg = new TSocketMessage(Encoding.Unicode.GetBytes(message));
            ClientSocket.SendMsg(msg,MessageType.command);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // 如果当前已经存在Socket，则关闭当前Socket
                if (ClientSocket != null)
                {
                    ClientSocket.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSendClientName_Click(object sender, RoutedEventArgs e)
        {
            string message = txtboxClientName.Text;
            txtboxInfo.Text = $"【发送客户端名称】 {message}\r\n" + txtboxInfo.Text;
            TSocketMessage msg = new TSocketMessage(Encoding.Unicode.GetBytes(message));
            ClientSocket.SendMsg(msg, MessageType.clientName);
        }
    }
}
