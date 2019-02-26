using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Palindrome
{

    public class IntroducingMethods
    {
        

        //命名参数
        public void DisplayGreeting(string firstName,string middleName = default(string),string lastName = default(string))
        {
            Console.WriteLine($"Hello, {firstName} {middleName} {lastName}");
        }

        //方法重载
        public void Method(int prompt)
        {
            Console.WriteLine(prompt.ToString());
        }

        public void Method(string prompt)
        {
            Console.WriteLine(prompt);
        }

        //递归函数
        public int Fabo(int xn)
        {
            if(xn == 1 || xn == 2)
            {
                return 1;
            }
            else
            {
                return Fabo(xn - 1) + Fabo(xn - 2);
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            IntroducingMethods introducingMethods = new IntroducingMethods();
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(introducingMethods.Fabo(i));
            }

            introducingMethods.DisplayGreeting(firstName: "Tommy", lastName: "Merlin");
            introducingMethods.Method(1);


            Console.Read();
        }
    }
}
