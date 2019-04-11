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

        // 创建连接到服务器的客户端字典（IP地址-Socket键值对）
        Dictionary<string, Socket> clientSockets = new Dictionary<string, Socket>();

        public Server()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 时刻保持监听客户端的连接请求
        /// </summary>
        public void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();    //连接到服务端的客户端Sokcet
                // 客户端字典中添加连接上的客户端
                clientSockets.Add(clientSocket.RemoteEndPoint.ToString(), clientSocket);

                // 新开线程时刻更新客户端列表
                Thread appendIpThread = new Thread(RefreshIpList)
                {
                    IsBackground = true
                };
                appendIpThread.Start();

                // 新开线程监听连接上的客户端的信息传输
                Thread receivedThread = new Thread(ReceiveMsg)
                {
                    IsBackground = true
                };
                receivedThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 更新客户端列表
        /// </summary>
        public void RefreshIpList()
        {
            lbConnectedIP.Dispatcher.BeginInvoke(new Action(() =>
            {
                lbConnectedIP.Items.Clear();

                // 将每一个非空且处于连接状态的Socket添加到列表中
                foreach(var socket in clientSockets)
                {
                    if (socket.Value.Connected && socket.Value != null)
                    {
                        string connectedClient = socket.Value.RemoteEndPoint.ToString();
                        TextBlock txtblock = new TextBlock
                        {
                            Text = connectedClient
                        };
                        lbConnectedIP.Items.Add(txtblock);
                    }
                }
            }));
        }

        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        /// <param name="clientSocket">连接上的客户端Socket</param>
        public void ReceiveMsg(object clientSocket)
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
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = $"【接收信息】 {sendStr}\r\n" + txtboxInfo.Text; }), null);

                }
                catch (Exception ex)
                {
                    // 出现异常，关闭连接
                    connection.Shutdown(SocketShutdown.Both);
                    connection.Close();
                    txtboxInfo.Dispatcher.BeginInvoke(

                            new Action(() => { txtboxInfo.Text = "\r\n" + $"【信息接收异常】 {ex.Message}\r\n" + txtboxInfo.Text; }), null);
                    break;
                }
            }
        }

        /// <summary>
        /// 服务端建立监听端口
        /// </summary>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            string path = "log.txt";                                     //日志路径
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            string logInfo;                                              //需要写入的日志信息

            try
            {
                IPAddress ip = IPAddress.Parse(txtboxIP.Text);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int port = Convert.ToInt32(txtboxPort.Text);

                // 绑定IP地址：端口
                serverSocket.Bind(new IPEndPoint(ip, port));
                //设定最多10个排队连接请求
                serverSocket.Listen(10);    
                txtboxIP.Text = ip.ToString();
                txtboxInfo.Text = "【服务器成功开启】\r\n" + txtboxInfo.Text;
                logInfo = $"【服务器成功开启】 IP:{ip} 端口号:{txtboxPort.Text} " + DateTime.Now.ToString();
                txtboxLog.Text = logInfo + "\r\n" + txtboxLog.Text;
                sw.WriteLine(logInfo);

                // 新开线程，监听客户端连接请求
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

        /// <summary>
        /// 发送信息
        /// </summary>
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
                            //socket.Value.Send(Encoding.Unicode.GetBytes(message));
                            byte[] buffer = Encoding.Unicode.GetBytes(message);

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

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbConnectedIP.Items.Clear();

            // 将每一个非空且处于连接状态的Socket添加到列表中
            foreach (var socket in clientSockets)
            {
                if (socket.Value.Connected && socket.Value != null)
                {
                    string connectedClient = socket.Value.RemoteEndPoint.ToString();
                    TextBlock txtblock = new TextBlock
                    {
                        Text = connectedClient
                    };
                    lbConnectedIP.Items.Add(txtblock);
                }
            }
        }
    }
}
