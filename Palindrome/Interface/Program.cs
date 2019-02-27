using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    interface IListable
    {
        string[] ColumnValues
        {
            get;
        }
    }

    interface IComparable
    {
        int CompareTo(object obj);
    }

    public abstract class PdaItem
    {
        public PdaItem(string name)
        {
            Name = name;
        }
        public virtual string Name { get; set; }
    }

    class Contact : PdaItem, IListable, IComparable
    {
        public Contact(string firstaName, string lastName, string phone, string address):base(null)
        {
            FirstaName = firstaName;
            LastName = lastName;
            Phone = phone;
            Address = address;
        }

        public string FirstaName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        string[] IListable.ColumnValues
        {
            get
            {
                return new string[]
                {
                    FirstaName,
                    LastName,
                    Phone,
                    Address
                };
            }
        }

        public string[] ColumnValues => new string[] { "Hello" };

        public int CompareTo(object obj)
        {
            int result;
            Contact contact = obj as Contact;
            if (obj == null)
            {
                result = 1;
            }
            else if (obj.GetType() != typeof(Contact))
            {
                throw new ArgumentException("E");
            }
            else
                result = 1;
            return result;
        }

        public static string[] Headers
        {
            get
            {
                return new string[]
                {
                    "First Name","Last Name","Phone","Address"
                };
            }
        }
    }

    class Publication:IListable
    {
        public Publication(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public string[] ColumnValues
        {
            get
            {
                return new string[]
                {
                    Title,
                    Author,
                    Year.ToString()
                };
            }
        }
    }

    class ConsoleListControl
    {
        public static void List(string[] headers,IListable[] items)
        {
            DisplayHeader(headers);
            for(int count = 0; count < items.Length; count++)
            {
                string[] values = items[count].ColumnValues;
                DisplayItemRow(values);
            }
        }

        public static void DisplayItemRow(string[] values)
        {
            foreach(string value in values)
            {
                Console.Write($"{value,20}");
            }
            Console.WriteLine();
        }

        public static void DisplayHeader(string[] headers)
        {
            foreach(string header in headers)
            {
                Console.Write($"{header,20}");
            }
            Console.WriteLine();
        }
    }

    class InterfaceProgram
    {
        static void Main(string[] args)
        {
            Contact[] contacts = new Contact[4];
            contacts[0] = new Contact("Tommy", "Merlin", "123-123-1234", "HangZhou");
            contacts[1] = new Contact("Dick", "Littleman", "555-123-4567", "NingBo");
            contacts[2] = new Contact("Marry", "Doe", "234-454-2334", "BeiJing");
            contacts[3] = new Contact("Jane", "Wilson", "234-566-4355", "ShangHai");
            string[] values = ((IListable)contacts[1]).ColumnValues;

            Publication[] publications = new Publication[3]
            {
                new Publication("Hello Word","Martin Smith",2019),
                new Publication("C# Programming","Sarah King",2000),
                new Publication("Tommorw","Tom Cooper",1999)
            };

            ConsoleListControl.List(Contact.Headers,contacts);

            Console.Read();
            
            
        }
    }
}
