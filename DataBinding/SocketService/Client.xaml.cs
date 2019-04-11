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

        private static Socket clientSocket = null;    //客户端Socket

        /// <summary>
        /// 接收服务端发来的信息
        /// </summary>
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
                    //显示内容
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = $"【接收信息】 {sendStr}\r\n" + txtboxInfo.Text; }), null);

                }
                catch (Exception ex)
                {
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + txtboxInfo.Text; }), null);
                    break;
                }
            }
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(txtboxIP.Text);
                int port = Convert.ToInt32(txtboxPort.Text);

                // 如果当前已经存在Socket，则先关闭当前Socket
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
                
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ip, port));
                txtboxInfo.Text = $"【连接成功】 我方端口 {clientSocket.LocalEndPoint.ToString()}\r\n" + txtboxInfo.Text;
                Thread th = new Thread(ReceiveMsg)
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("【连接失败】 " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                string message = txtboxMessage.Text;
                

                byte[] buffer = Encoding.Unicode.GetBytes(message);

                for (int i = 0; i < 2; i++)
                {
                    txtboxInfo.Text = $"【发送消息】 {message}\r\n" + txtboxInfo.Text;
                    buffer = ProtocolHelper.PackData(buffer);
                    clientSocket.Send(buffer);

                }

                //for (int i = 0; i < 100; i++)
                //{
                //    txtboxInfo.Text = $"【发送消息】 {message}\r\n" + txtboxInfo.Text;
                //    clientSocket.Send(buffer);
                //}
                //clientSocket.Send(buffer);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // 如果当前已经存在Socket，则关闭当前Socket
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
