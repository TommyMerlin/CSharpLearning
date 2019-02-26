using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class EmployeeWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
        public static int NextId = 42;
        public string ID { get; set; }

        //构造器
        public EmployeeWithId(string firstName,string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = "Not Enough";
            ID = NextId.ToString();
        }

        static EmployeeWithId()
        {
            Random rnd = new Random();
            NextId = rnd.Next(101, 999);
        }
    }

    class AcessModifierProgram
    {
        static void Main(string[] args)
        {
            EmployeeWithId employee1 = new EmployeeWithId("Tommy", "Merlin");
            Console.WriteLine("{0} {1}: {2}({3})", employee1.FirstName, employee1.LastName, employee1.Salary,employee1.ID);
            
            

            Console.ReadLine();
        }
    }
}
