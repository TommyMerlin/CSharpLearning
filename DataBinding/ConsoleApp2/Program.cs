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
        

        public static int Max(int[] Lists,int count,out int begin,out int end)
        {
            int thisSum = 0;
            int maxSum = 0;
            int maybeBegin = 0;
            begin = 0;
            end = count;

            int flag = 1;
            int max_flag = 1;

            for (int i = 0; i < count; i++)
            {
                thisSum += Lists[i];
                if (thisSum > maxSum)
                {
                    max_flag = -max_flag;
                    if (flag > 0)
                    {
                        begin = maybeBegin;
                        flag = -flag;
                    }
                    maxSum = thisSum;
                    end = i;
                }
                else if (thisSum < 0)
                {
                    maybeBegin = i + 1;
                    thisSum = 0;
                    flag = 1;
                    //end = i;
                    //flag = -flag;
                }
            }
            return maxSum;
        }

        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            string[] strs = new string[count];
            string input = Console.ReadLine();

            strs = input.Split(' ');

            int[] nums = new int[count];

            for (int i = 0; i < count; i++)
            {
                nums[i] = Convert.ToInt32(strs[i]);
            }

            int beginIndex, endIndex;

            int MaxSum = Max(nums, count, out beginIndex, out endIndex);
            Console.WriteLine("{0} {1} {2}",MaxSum,beginIndex,endIndex);

            Console.Read();
        }

        
    }
}
