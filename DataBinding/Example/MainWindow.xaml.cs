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

namespace Example
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 创建一个和客户端通信的套接字
        static Socket serverSocket = null;
        static List<Socket> sockets = new List<Socket>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //监听客户端发来的请求  
        public void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                sockets.Add(clientSocket);
                //为接受数据创建一个线程
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
        public void ReceiveMessage(object clientSocket)
        {
            Socket connection = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    byte[] result = new byte[1024];
                    //通过clientSocket接收数据  
                    int receiveNumber = connection.Receive(result);
                    //把接受的数据从字节类型转化为字符类型
                    String recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);


                    //获取当前客户端的ip地址
                    IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                    //获取客户端端口
                    int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                    String sendStr = clientIP + ":" + clientPort.ToString() + "--->" + recStr;
                    foreach (Socket socket in sockets)
                    {
                        socket.Send(Encoding.Unicode.GetBytes(sendStr));
                    }
                    //显示内容
                    text1.Dispatcher.BeginInvoke(

                            new Action(() => { text1.Text += "\r\n" + sendStr; }), null);

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
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, 6002));  //绑定IP地址：端口  
            serverSocket.Listen(10);    //设定最多10个排队连接请求             
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client c = new Client();
            c.Show();
        }
    }
}
