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
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;

namespace SocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Server : Window
    {
        // 创建TCP监听对象
        Socket serverSocket = null;
        //List<Socket> clientSockets = new List<Socket>();
        Dictionary<string, Socket> clientSockets = new Dictionary<string, Socket>();

        
        
        public Server()
        {
            InitializeComponent();
            
        }

        public void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSockets.Add(clientSocket.RemoteEndPoint.ToString(), clientSocket);

                //IPAddress clientIP = (clientSocket.RemoteEndPoint as IPEndPoint).Address;
                //int clientPort = (clientSocket.RemoteEndPoint as IPEndPoint).Port;
                //string connectedClient = clientIP + ":" + clientPort.ToString();
                //TextBlock txtblock = new TextBlock();
                //txtblock.Text = connectedClient;
                //lbConnectedIP.Items.Add(txtblock);
                Thread appendIpThread = new Thread(AppendIpList);
                appendIpThread.IsBackground = true;
                appendIpThread.Start();

                Thread receivedThread = new Thread(ReceiveMsg);
                receivedThread.IsBackground = true;
                receivedThread.Start(clientSocket);
            }
        }

        public void AppendIpList()
        {
            lbConnectedIP.Dispatcher.BeginInvoke(new Action(() =>
            {
                lbConnectedIP.Items.Clear();
                foreach(var socket in clientSockets)
                {
                    if (socket.Value.Connected && socket.Value != null)
                    {
                        //IPAddress clientIP = (socket.RemoteEndPoint as IPEndPoint).Address;
                        //int clientPort = (socket.RemoteEndPoint as IPEndPoint).Port;
                        string connectedClient = socket.Value.RemoteEndPoint.ToString();
                        TextBlock txtblock = new TextBlock
                        {
                            Text = connectedClient
                        };
                        lbConnectedIP.Items.Add(txtblock);
                    }
                    else
                    {
                        //clientSockets
                    }
                }
            }));

            //lbConnectedIP.Items.Add(txtblock);
        }

        public void ReceiveMsg(object clientSocket)
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
                    string recStr = Encoding.Unicode.GetString(result, 0, receiveNumber);


                    //获取当前客户端的ip地址
                    IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                    //获取客户端端口
                    int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
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
                    connection.Shutdown(SocketShutdown.Both);
                    connection.Close();
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + txtboxInfo.Text; }), null);
                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //string path = @"F:\Desktop\log.txt";
            string path = "log.txt";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            string logInfo;

            try
            {
                IPAddress ip = IPAddress.Parse(txtboxIP.Text);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int port = Convert.ToInt32(txtboxPort.Text);
                serverSocket.Bind(new IPEndPoint(ip, port));  //绑定IP地址：端口  
                serverSocket.Listen(10);    //设定最多10个排队连接请求
                txtboxIP.Text = ip.ToString();
                txtboxInfo.Text = "【服务器成功开启】\r\n" + txtboxInfo.Text;
                logInfo = $"【服务器成功开启】 IP:{ip} 端口号:{txtboxPort.Text} " + DateTime.Now.ToString();
                txtboxLog.Text = logInfo + "\r\n" + txtboxLog.Text;
                sw.WriteLine(logInfo);

                //ip = IPAddress.Parse("127.0.0.1");
                ////ip = Dns.GetHostEntry("localhost").AddressList[0];
                //server = new TcpListener(ip, Convert.ToInt32(txtboxPort.Text));
                //server.Start();
                //txtboxIP.Text = ip.ToString();
                //txtboxInfo.Text = "Server started...\r\n" + txtboxInfo.Text;
                //logInfo = $"【服务器成功开启】 IP:{ip} 端口号:{txtboxPort.Text} " + DateTime.Now.ToString();
                //txtboxLog.Text = logInfo + "\r\n" + txtboxLog.Text;
                //sw.WriteLine(logInfo);

                Thread th = new Thread(ListenClientConnect)
                {
                    Name = "接收消息",
                    IsBackground = true
                };
                th.Start();
                btnStart.IsEnabled = false;
            }
            catch (Exception ex)
            {
                txtboxInfo.Text = $"【服务器启动异常】 {ex.Message}\r\n" + txtboxInfo.Text;
                logInfo = $"【服务器启动异常】 {ex.Message} " + DateTime.Now.ToString();
                txtboxLog.Text = logInfo + "\r\n" + txtboxLog.Text;
                sw.WriteLine(logInfo);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = txtboxMessage.Text;
                txtboxInfo.Text = $"【发送消息】 {message}\r\n" + txtboxInfo.Text;

                //foreach (Socket socket in clientSockets)
                //{
                //    if (socket != null && socket.Connected)
                //    {
                //        socket.Send(Encoding.Unicode.GetBytes(message));
                //    }

                //}

                if (lbConnectedIP.SelectedItem == null)
                {
                    foreach (var socket in clientSockets)
                    {
                        if (socket.Value != null && socket.Value.Connected)
                        {
                            socket.Value.Send(Encoding.Unicode.GetBytes(message));
                        }

                    }
                }
                else
                {
                    string ip = ((TextBlock)lbConnectedIP.SelectedItem).Text;
                    clientSockets[ip].Send(Encoding.Unicode.GetBytes(message));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCreateClient_Click(object sender, RoutedEventArgs e)
        {
            Client c = new Client();
            c.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Environment.Exit(0);
        }
    }
}
