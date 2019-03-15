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
        public static List<int> SelectSort(List<int> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                int min = lists[i];
                int minindex = i;
                for(int j = i; j < lists.Count; j++)
                {
                    if (min > lists[j])
                    {
                        min = lists[j];
                        minindex = j;
                    }
                }
                int temp = lists[i];
                lists[i] = lists[minindex];
                lists[minindex] = temp;
            }

            return lists;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<int> lists = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                lists.Add(rnd.Next(0, 10));
            }
            lists = Program.SelectSort(lists);
            foreach(int num in lists)
            {
                Console.WriteLine(num);
            }

            Console.ReadKey();
        }

        
    }
}
