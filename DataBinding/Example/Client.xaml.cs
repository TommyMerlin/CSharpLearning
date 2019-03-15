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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Example
{
    /// <summary>
    /// Client.xaml 的交互逻辑
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
            string text = text1.Text;
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(ip, 6002));
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);
        }

        public void ReceiveMessage(object clientSocket)
        {
            Socket connection = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //接受数据
                    byte[] result = new byte[1024];
                    //通过clientSocket接收数据  
                    int receiveNumber = connection.Receive(result);
                    //把接受的数据从字节类型转化为字符类型
                    String recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);
                    text2.Dispatcher.BeginInvoke(

                           new Action(() => { text2.Text += "\r\n" + recStr; }), null);

                }
                catch (Exception ex)
                {

                    connection.Shutdown(SocketShutdown.Both);
                    connection.Close();
                    break;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String text = text1.Text;
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 6002)); //配置服务器IP与端口  

            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            clientSocket.Send(Encoding.Unicode.GetBytes(text));



            //通过 clientSocket 发送数据  

            //clientSocket.Close();

        }
    }
}
