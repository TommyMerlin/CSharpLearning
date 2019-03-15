using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        private int cnt = 0;
        public void Count()
        {
            while(cnt < 10)
            {
                cnt++;
                Console.WriteLine(Thread.CurrentThread.Name + " 数到: " + cnt);
                Thread.Sleep(100);
            }
        }

        static void Main(string[] args)
        {
            //GreetingPeople("HuYe", EnglishGreeting);
            //GreetingPeople("HuYe", ChineseGreeting);

            //GreetingManager gm = new GreetingManager();
            //gm.MakeGreet += ChineseGreeting;
            //gm.MakeGreet += EnglishGreeting;
            //gm.GreetingPeople("Huye");

            //Heater heater = new Heater();
            //Alarm alarm = new Alarm();
            //Display display = new Display();

            Program g1 = new Program();
            Program g2 = new Program();

            Thread th1 = new Thread(new ThreadStart(g1.Count));
            th1.Name = "线程1";
            Thread th2 = new Thread(new ThreadStart(g2.Count));
            th2.Name = "线程2";
            Thread th3 = new Thread(new ThreadStart(g2.Count));
            th3.Name = "线程3";
            th2.Priority = ThreadPriority.Highest;

            th1.Start();
            th2.Start();
            th3.Start();

            Console.ReadKey();
        }

        
    }
}
