using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Enum
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        public enum ConnecttionState
        {
            Disconnected,
            Connecting,
            Connected,
            Disconnecting
        }

        /// <summary>
        /// Execute codes according to the condition.
        /// </summary>
        /// <remarks>
        /// This method uses
        /// <seealso cref="System.IO.FileStream"/>
        /// in addition to
        /// <seealso cref="System.IO.StreamWriter"/>
        /// </remarks>
        /// <param name="connecttionState">State of Connection</param>
        /// <date>January 28, 2019</date>
        public void Operate(ConnecttionState connecttionState)
        {
            switch (connecttionState)
            {
                case ConnecttionState.Connected:
                    Console.WriteLine("Connected");
                    break;
                case ConnecttionState.Connecting:
                    Console.WriteLine("Connecting");
                    break;
                default:
                    Console.WriteLine("Hello");
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(1<<2);

            Console.Read();
        }
    }
}
