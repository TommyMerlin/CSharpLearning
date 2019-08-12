using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genericity
{
    public class GenericClass<T>
    {
        T[] TArray = new T[10];
        public int position = 0;
        public void push(T t)
        {
            position++;
            TArray[position] = t;
        }
        public T push()
        {
            position--;
            return TArray[position];
        }
        public T[] GetTs() => TArray;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var gClass = new GenericClass<string>();
            gClass.push("ZJU");
            gClass.push("YQ");
            for (int i = 0; i < gClass; i++)
            {
                Console.WriteLine(gClass[i]);
            }
        }
    }
}
