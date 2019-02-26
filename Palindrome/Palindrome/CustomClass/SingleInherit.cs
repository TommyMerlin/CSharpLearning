using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    public class ClassRoom
    {
        public ClassRoom(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public virtual void GetName()
        {
            Console.WriteLine($"{Name}");
        }
    }

    public class Student:ClassRoom
    {
        public Student(string name) : base(name)
        {
        }

        public override void GetName()
        {
            Console.WriteLine($"Student's name is {Name}");
        }
    }

    class SingleInheritProgram
    {
        static void Main(string[] args)
        {
            Student student = new Student("Tommy Merlin");
            student.GetName();
        }
    }

    
}
