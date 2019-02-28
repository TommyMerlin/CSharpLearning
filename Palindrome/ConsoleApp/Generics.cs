using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Game
    {
        public static void Beep()
        {
            Console.Write("请输入频率：");
            int frequency = int.Parse(Console.ReadLine());
            while (frequency > 37 && frequency < 37000)
            {
                Console.Beep(frequency, 2000);
                Console.Write("请输入频率：");
                frequency = int.Parse(Console.ReadLine());
            }
        }

        static void Main(string[] args)
        {
            Beep();
        }
    }
}
