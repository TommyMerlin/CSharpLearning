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

    public abstract class PdaItem
    {
        public PdaItem(string name)
        {
            Name = name;
        }
        public virtual string Name { get; set; }
    }

    class Contact : PdaItem, IListable
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

        public  string[] ColumnValues
        {
            get
            {
                return new string[]
                {
                    FirstaName,
                    LastName
                };
            }
        }

        public static string[] Headers
        {
            get
            {
                return new string[]
                {
                    "First Name","Last Name  ",
                    "Phone      ",
                    "Address                    "
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
            int[] columnWidths = new int[4];
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

            
            
        }
    }
}
