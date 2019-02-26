using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Palindrome
{
    class Item
    {
        public string Name;
        public string Id;

        public Item(string name, string id)
        {
            Name = name;
            Id = id;
            Console.WriteLine("Item Class Constructor!");
        }
    }

    class Contact : Item
    {
        public string Address;

        public Contact(string name, string id, string address) : base(name, id)
        {
            Address = address;
            Console.WriteLine("Contact Class Constructor!");
        }
    }

    class Custom : Contact
    {
        public string Phone;

        public Custom(string name, string id, string address,string phone):base(name,id,address)
        {
            Phone = phone;
            Console.WriteLine("Custom Class Constructor!");
        }
    }

    class InheritProgram
    {
        static void Main(string[] args)
        {
            Item item = new Item("Tommy", "Merlin");
            Console.WriteLine();
            Contact contact = new Contact("Tommy", "Merlin", "HangZhou");
            Console.WriteLine();
            Custom custom = new Custom("Tommy", "Merlin", "HangZhou", "18867542193");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
