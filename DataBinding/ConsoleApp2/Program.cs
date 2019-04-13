// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/3/16 21:20:35
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Astar;
using System.Data;

namespace Drawing
{
    public class Store
    {
        public int coe { get; set; }
        public int index { get; set; }
    }


    public class Program
    {
        static void Main()
        {
            
            Task task = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.Write("-");
                }
            }
            );

            for (int i = 0; i < 1000; i++)
            {
                Console.Write("+");
            }
            //task.Wait();

            Console.Read();
        }
    }
}
