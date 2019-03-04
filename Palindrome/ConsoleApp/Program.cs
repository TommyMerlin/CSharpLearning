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
        
        public enum ConnecttionState
        {
            Disconnected,
            Connecting,
            Connected,
            Disconnecting
        }


        // ********************************************************************************
        /// <summary>
        /// Operates the specified connecttion state.
        /// </summary>
        /// <param name="connecttionState">State of the connecttion.</param>
        /// <param name="name">The name.</param>
        /// <created>HuYe,2019/2/28</created>
        /// <changed>HuYe,2019/2/28</changed>
        // ********************************************************************************
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
            Console.Beep(300, 2000);

            Console.Read();
        }
    }
}
