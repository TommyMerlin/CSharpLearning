using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 创建IP地址
        IPAddress ip;
        // 创建TCP监听对象
        TcpListener listener;
        TcpClient client;

        private delegate void UpdateUIDelegate();

        public MainWindow()
        {
            InitializeComponent();

            UpdateUIThreadTest();
        }

        private void UpdateUIThreadTest()
        {
            var th = new Thread(ThreadUpdateUI);
            th.Start();
        }

        private void ThreadUpdateUI()
        {
            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(UpdateUI);
            this.Dispatcher.Invoke(updateUIDelegate);
            
        }

        




        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //UpdateUIThreadTest();
        }

        public void Server()
        {
            ip = IPAddress.Parse(txtboxIP.Text);
            listener = new TcpListener(ip, Convert.ToInt32(txtboxPort.Text));
            listener.Start();
            txtboxInfo.Text = "服务器监听启动 " + DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;
            btnStart.IsEnabled = false;

            // 中断，等待
            client = listener.AcceptTcpClient();
            txtboxInfo.Text = $"连接成功,对方端口 {client.Client.RemoteEndPoint.ToString()} " + DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;

            while (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                byte[] byteArray = new byte[1024];
                int length = stream.Read(byteArray, 0, 1024);
                string message = Encoding.Unicode.GetString(byteArray, 0, length);
                txtboxInfo.Text = $"接收信息: {message} " + DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;
            }
        }
    }
}
