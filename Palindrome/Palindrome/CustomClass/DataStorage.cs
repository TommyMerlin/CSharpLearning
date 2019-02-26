using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Palindrome
{
    class Employee
    {
        public string FirstName;
        public string LastName;
        public string Salary = "None";
        public string GetName()
        {
            return $"{FirstName} {LastName}";
        }

        public void SetName(string newFirstName,string newLastName)
        {
            FirstName = newFirstName;
            LastName = newLastName;
            Console.WriteLine($"Name changed to '{this.GetName()}'");
        }

        public void Save()
        {
            DataStorage.Store(this);
        }
    }

    class DataStorage
    {
        public static void Store(Employee employee)
        {
            FileStream stream = new FileStream(employee.FirstName + employee.LastName + ".txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(employee.FirstName);
            writer.WriteLine(employee.LastName);
            writer.WriteLine(employee.Salary);
            writer.Close();
        }

        public static Employee Load(string firstName,string lastName)
        {
            string filePath = firstName + lastName + ".txt";
            Employee employee = new Employee();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            employee.FirstName = reader.ReadLine();
            employee.LastName = reader.ReadLine();
            employee.Salary = reader.ReadLine();
            reader.Close();

            return employee;
        }
    }

    class FileStreamClass
    {
        static void Main(string[] args)
        {
            Employee employee1 = new Employee();
            employee1.SetName("Tommy", "Merlin");
            employee1.Save();

            Employee employee2;
            employee2 = DataStorage.Load("Tommy", "Merlin");
            Console.WriteLine(employee2.GetName());

            Console.Read();
        }
    }
}
