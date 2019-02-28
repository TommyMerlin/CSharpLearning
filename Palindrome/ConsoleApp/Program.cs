using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    // --------------------------------------------------------------------------------
    
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

        
        public void Operate(ConnecttionState connecttionState,string name)
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
