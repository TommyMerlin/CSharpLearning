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

namespace SocketClient
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

        TcpClient client;

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            client = new TcpClient();
            
            try
            {
                client.Connect(txtboxIP.Text, Convert.ToInt32(txtboxPort.Text));
                if (client.Connected)
                {
                    txtboxInfo.Text = $"连接成功,我方端口 {client.Client.LocalEndPoint.ToString()} " +
                        DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;
                }
                else
                {
                    txtboxInfo.Text = "连接失败" + DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("连接失败-" + ex.Message);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = txtboxMessage.Text;
                if(message != string.Empty)
                {
                    txtboxMessage.Text = string.Empty;
                    txtboxInfo.Text = $"发送消息: {message} " + DateTime.Now.ToShortTimeString() + "\r\n" + txtboxInfo.Text;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = Encoding.Unicode.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    MessageBox.Show("请输入要发送的内容");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
