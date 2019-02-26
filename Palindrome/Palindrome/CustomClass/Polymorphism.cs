using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace Palindrome
{
    public abstract class PdaIem
    {
        protected PdaIem(string name)
        {
            Name = name;
        }

        public abstract string GetSummary();
        public virtual string Name { get; set; }
    }

    public class Contactor:PdaIem
    {
        public Contactor(string name):base(name)
        {
            Name = name;
        }

        public override string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            set
            {
                string[] names = value.Split(' ');
                FirstName = names[0];
                LastName = names[1];
            }
        }
        public override string GetSummary()
        {
            return $"FirstName: {FirstName + NewLine}" + $"LastName: {LastName + NewLine}" + $"Address: {Address + NewLine}";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class Appointment:PdaIem
    {
        public Appointment(string name) : base(name)
        {
            Name = name;
        }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string GetSummary()
        {
            return $"Subject: {Name + NewLine}" + $"Start: {BeginDate + NewLine}" + $"End: {EndDate + NewLine}";
        }
    }

    class PloyProgram
    {
        static void Main(string[] args)
        {
            PdaIem[] pdaIems = new PdaIem[2];
            Contactor contact = new Contactor("Shelock Holmes");
            contact.Address = "Bacer Street";
            Appointment appointment = new Appointment("Soccer tournament");
            appointment.BeginDate = new DateTime(2019, 1, 1);
            appointment.EndDate = new DateTime(2019, 1, 7);
            pdaIems[0] = contact;
            pdaIems[1] = appointment;
            List(pdaIems);

            Console.Read();
        }

        public static void List(PdaIem[] items)
        {
            foreach(PdaIem item in items)
            {
                Console.WriteLine(item.GetSummary());
            }
        }
    }
}
