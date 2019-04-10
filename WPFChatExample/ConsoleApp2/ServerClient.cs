// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/10 15:15:38
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ConsoleApp2
{
    class ServerClient
    {
        private const int BufferSize = 8192;
        private byte[] buffer;
        private TcpClient client;
        private NetworkStream streamToServer;

        public ServerClient()
        {
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 8500);      // 与服务器连接
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            buffer = new byte[BufferSize];

            // 打印连接到的服务端信息
            Console.WriteLine("Server Connected！{0} --> {1}",
                client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

            streamToServer = client.GetStream();
        }

        // 发送消息到服务端
        public void SendMessage(string msg)
        {

            byte[] temp = Encoding.Unicode.GetBytes(msg);   // 获得缓存
            try
            {
                lock (streamToServer)
                {
                    streamToServer.Write(temp, 0, temp.Length); // 发往服务器
                }
                Console.WriteLine("Sent: {0}", msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        // 发送文件 - 异步方法
        public void BeginSendFile(string filePath)
        {
            ParameterizedThreadStart start =
                new ParameterizedThreadStart(BeginSendFile);
            start.BeginInvoke(filePath, null, null);
        }

        private void BeginSendFile(object obj)
        {
            string filePath = obj as string;
            SendFile(filePath);
        }

        // 发送文件 -- 同步方法
        public void SendFile(string filePath)
        {

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 0);
            listener.Start();

            // 获取本地侦听的端口号
            IPEndPoint endPoint = listener.LocalEndpoint as IPEndPoint;
            int listeningPort = endPoint.Port;

            // 获取发送的协议字符串
            string fileName = Path.GetFileName(filePath);
            FileProtocol protocol =
                new FileProtocol(FileRequestMode.Send, listeningPort, fileName);
            string pro = protocol.ToString();

            SendMessage(pro);       // 发送协议到服务端

            // 中断，等待远程连接
            TcpClient localClient = listener.AcceptTcpClient();
            Console.WriteLine("Start sending file...");
            NetworkStream stream = localClient.GetStream();

            // 创建文件流
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] fileBuffer = new byte[1024];     // 每次传1KB
            int bytesRead;
            int totalBytes = 0;

            // 创建获取文件发送状态的类
            SendStatus status = new SendStatus(filePath);

            // 将文件流转写入网络流
            try
            {
                do
                {
                    Thread.Sleep(10);           // 为了更好的视觉效果，暂停10毫秒:-)
                    bytesRead = fs.Read(fileBuffer, 0, fileBuffer.Length);
                    stream.Write(fileBuffer, 0, bytesRead);
                    totalBytes += bytesRead;            // 发送了的字节数
                    status.PrintStatus(totalBytes); // 打印发送状态
                } while (bytesRead > 0);
                Console.WriteLine("Total {0} bytes sent, Done!", totalBytes);
            }
            catch
            {
                Console.WriteLine("Server has lost...");
            }

            stream.Dispose();
            fs.Dispose();
            localClient.Close();
            listener.Stop();
        }
    }

    // 即时计算发送文件的状态
    public class SendStatus
    {
        private FileInfo info;
        private long fileBytes;

        public SendStatus(string filePath)
        {
            info = new FileInfo(filePath);
            fileBytes = info.Length;
        }

        public void PrintStatus(int sent)
        {
            string percent = GetPercent(sent);
            Console.WriteLine("Sending {0} bytes, {1}% ...", sent, percent);
        }

        // 获得文件发送的百分比
        public string GetPercent(int sent)
        {

            decimal allBytes = Convert.ToDecimal(fileBytes);
            decimal currentSent = Convert.ToDecimal(sent);

            decimal percent = (currentSent / allBytes) * 100;
            percent = Math.Round(percent, 1);   //保留一位小数

            if (percent.ToString() == "100.0")
                return "100";
            else
                return percent.ToString();
        }
    }
}
