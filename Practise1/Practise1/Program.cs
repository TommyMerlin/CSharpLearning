using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Practise1
{
    class ParentClass
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public ParentClass()
        {
            Age = 12;
            Name = "HUYE";
        }

        public ParentClass(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }

    class ChildClass : ParentClass
    {
        public int Height { get; set; }

        public ChildClass()
        {
            Height = 178;
        }

        public ChildClass(int age,string name)
        {
            Height = 180;
        }
    }

    public abstract class BaseClass
    {
        public abstract void Print();
    }

    public class ExtendClass : BaseClass
    {
        public virtual void Display() { }

        public override void Print()
        {
            Console.WriteLine("Hello World!");
        }
    }

    class Program
    {
        public delegate bool ComparisonHandler(int first, int second);

        public static bool AlphabetGreaterThan(int first, int second)
        {
            int comparison;
            comparison = first.ToString().CompareTo(second.ToString());
            return comparison > 0;
        }

        public static void BubbleSort(int[] arr, ComparisonHandler ComparisonMethod)
        {
            if (arr == null)
            {
                return;
            }
            int temp = 0;
            for (int i = arr.Length-1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (ComparisonMethod(arr[j-1],arr[j]))
                    {
                        temp = arr[j-1];
                        arr[j-1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            ChildClass c1 = new ChildClass();
            ChildClass c2 = new ChildClass(13, "ZJU");
            Console.WriteLine($"{c1.Name}:{c1.Age} {c1.Height}");
            Console.WriteLine($"{c2.Name}:{c2.Age} {c2.Height}");

            Console.Read();
        }
    }
}
