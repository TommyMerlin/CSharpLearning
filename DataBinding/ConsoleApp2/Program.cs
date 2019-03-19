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
        public static void Operate(List<Store> stores, List<Store> stores1)
        {

        }

        static void Main(string[] args)
        {
            string[] list = Console.ReadLine().Split(' ');
            string[] list1 = Console.ReadLine().Split(' ');
            int[] nums = new int[list.Length];
            int[] nums1 = new int[list1.Length];

            for (int i = 0; i < list.Length; i++)
            {
                nums[i] = Convert.ToInt32(list[i]);
            }
            List<Store> stores = new List<Store>();
            for(int i = 0; i <= nums[0]; i++)
            {
                Store s = new Store()
                {
                    coe = nums[2 * i],
                    index = nums[2 * i + 1]

                };
                stores.Add(s);
            }

            for (int i = 0; i < list1.Length; i++)
            {
                nums1[i] = Convert.ToInt32(list1[i]);
            }
            List<Store> stores1 = new List<Store>();
            for (int i = 0; i <= nums[0]; i++)
            {
                Store s = new Store()
                {
                    coe = nums1[2 * i],
                    index = nums1[2 * i + 1]

                };
                stores1.Add(s);
            }

            Operate(stores, stores1);

            Console.Read();
        }
    }
}
