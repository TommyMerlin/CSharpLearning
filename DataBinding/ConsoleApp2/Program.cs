using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread th = new Thread(new ThreadStart(Test));
            th.IsBackground = true;
            th.Start();
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine("***:" + i);
            }

            Console.ReadKey();
        }

        public static void Test()
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine("---:" + i);
            }
        }
    }
}
