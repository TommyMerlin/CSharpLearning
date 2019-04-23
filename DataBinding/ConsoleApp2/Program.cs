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
    public class Longitude
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Longitude(int x,int y)
        {
            X = x;
            Y = y;
        }

        public static Longitude operator +(Longitude target1,Longitude target2)
        {
            Longitude longitude = new Longitude(target1.X + target2.X, target1.Y + target2.Y);
            
            return longitude;
        }
    }
    
    public class Program
    {
        static void Main()
        {
            Longitude l1 = new Longitude(1, 1);
            Longitude l2 = new Longitude(1, 1);
            l1 = l1 + l2;
            Console.WriteLine($"{l1.X} {l1.Y}");

            Console.Read();
        }
    }
}
