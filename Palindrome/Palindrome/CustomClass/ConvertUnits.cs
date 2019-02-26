using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    partial class ConvertUnits
    {
        public const float CentimeterPerInch = 2.45F;
        public const int CupsPerGallon = 16;
        public readonly int _Id;
        public string FirstName;

        public ConvertUnits(int Id)
        {
            _Id = Id;
        }

        public ConvertUnits(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id => _Id;
    }

    class ConvertUnitsProgram
    {
        static void Main(string[] args)
        {
            ConvertUnits cu = new ConvertUnits(10);
            Console.WriteLine(cu.Id);
            //cu.Id = 1;
            ConvertUnits cu1 = new ConvertUnits("Tommy", "Merlin");
            cu1.GetFullName();

            Console.Read();
        }
    }
}
