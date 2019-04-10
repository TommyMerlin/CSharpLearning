using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;

            ServerClient client = new ServerClient();
            string filePath = Environment.CurrentDirectory + "/" + "1.jpg";

            if (File.Exists(filePath))
                client.BeginSendFile(filePath);

            Console.WriteLine("\n\n输入\"Q\"键退出。");
            do
            {
                key = Console.ReadKey(true).Key;
                filePath = Environment.CurrentDirectory + "/" + "1.jpg";

                if (File.Exists(filePath))
                    client.BeginSendFile(filePath);
            } while (key != ConsoleKey.Q);
        }
    }
}
