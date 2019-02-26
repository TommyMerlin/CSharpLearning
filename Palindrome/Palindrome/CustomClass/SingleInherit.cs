using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    public class ClassRoom
    {
        public virtual string Name { get; set; }
    }

    public class Student:ClassRoom
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }

    class SingleInheritProgram
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            student.FirstName = "Tommy";
            student.LastName = "Merlin";
            Console.WriteLine(student.Name);

            Console.Read();
        }
    }

    
}
