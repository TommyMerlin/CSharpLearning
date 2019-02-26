using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Palindrome
{
    class ExceptionHandling
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("begin");
                Console.WriteLine("throw an exception");
                throw new Exception("Hello");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }
    }
}
