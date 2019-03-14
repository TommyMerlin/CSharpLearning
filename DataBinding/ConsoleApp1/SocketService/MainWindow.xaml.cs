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

namespace SocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 创建IP地址
        IPAddress ip;
        // 创建TCP监听对象
        TcpListener listener;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            ip = IPAddress.Parse(txtboxIP.Text);
            listener = new TcpListener(ip, Convert.ToInt32(txtboxPort.Text));
            listener.Start();
            txtboxInfo.Text = "服务器监听启动 " + DateTime.Now.ToString() + "\r\n" + txtboxInfo.Text;
        }
    }
}
