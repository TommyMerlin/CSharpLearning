using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class Out
    {
        static void Main(string[] args)
        {
            string firstName;
            string lastName;
            //string fullName = GetUserInput(out firstName, out lastName);
            //Console.WriteLine(firstName);
            //Console.WriteLine(lastName);
            //Console.WriteLine(fullName);

            string ageText = Console.ReadLine();
            if(int.TryParse(ageText,out int age))
            {
                Console.WriteLine($"Your are {age} years old");
            }
            else
            {
                Console.WriteLine("Error");
            }

            Console.Read();
        }
        
        static string GetUserInput(out string firstName, out string lastName)
        {
            firstName = Console.ReadLine();
            lastName = Console.ReadLine();
            return firstName + " " + lastName;
        }
    }
}
