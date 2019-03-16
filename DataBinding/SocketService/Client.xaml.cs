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
        public Client()
        {
            InitializeComponent();

        }

        private static Socket clientSocket = null;

        public void ReceiveMsg()
        {
            while (true)
            {
                try
                {
                    byte[] result = new byte[1024];
                    //通过clientSocket接收数据  
                    int receiveNumber = clientSocket.Receive(result);
                    //把接受的数据从字节类型转化为字符类型
                    string recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);


                    //获取当前客户端的ip地址
                    IPAddress clientIP = (clientSocket.RemoteEndPoint as IPEndPoint).Address;
                    //获取客户端端口
                    int clientPort = (clientSocket.RemoteEndPoint as IPEndPoint).Port;
                    string sendStr = clientIP + ":" + clientPort.ToString() + "--->" + recStr;
                    //foreach (Socket socket in clientSockets)
                    //{
                    //    socket.Send(Encoding.Unicode.GetBytes(sendStr));
                    //}
                    //显示内容
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = $"【接收信息】 {sendStr}\r\n" + txtboxInfo.Text; }), null);

                }
                catch (Exception ex)
                {
                    //clientSocket.Shutdown(SocketShutdown.Both);
                    //clientSocket.Close();
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + txtboxInfo.Text; }), null);
                    break;
                }
            }
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            //client = new TcpClient();
            ////设定服务器IP地址  
            //IPAddress ip = IPAddress.Parse(txtboxIP.Text);
            //int port = Convert.ToInt32(txtboxPort.Text);
            //clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPAddress ip = IPAddress.Parse(txtboxIP.Text);
                int port = Convert.ToInt32(txtboxPort.Text);

                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
                
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ip, port));
                txtboxInfo.Text = $"【连接成功】 我方端口 {clientSocket.LocalEndPoint.ToString()}\r\n" + txtboxInfo.Text;
                Thread th = new Thread(ReceiveMsg);
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show("【连接失败】 " + ex.Message);
                return;
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = txtboxMessage.Text;
                txtboxInfo.Text = $"【发送消息】 {message}\r\n" + txtboxInfo.Text;
                clientSocket.Send(Encoding.Unicode.GetBytes(message));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (clientSocket != null)
                {
                    //clientSocket.Shutdown(SocketShutdown.Both);
                    //Thread.Sleep(10);
                    clientSocket.Close();
                }
                
                //Thread.CurrentThread.Abort();
                //clientSocket.BeginDisconnect(true, null, null);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
