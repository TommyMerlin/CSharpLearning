using System;
using System.IO;


namespace Palindrome
{
    public static class SimpleMath
    {
        public static int Max(params int[] nums)
        {
            if(nums.Length == 0)
            {
                //throw new ArgumentException("numbers cannot be empty", "numbers");
            }
            int result = nums[0];
            foreach(int num in nums)
            {
                if (num > result)
                {
                    result = num;
                }
            }
            return result;
        }
    }

    partial class ConvertUnits
    {
        public string LastName;
        public string FullName;

        public void GetFullName()
        {
            Console.WriteLine($"{FirstName} {LastName}");
        }
    }

    public class StaticClassProgram
    {
        static void Main(string[] args)
        {
            string path = @"F:\GitHub\AGV-Path-Planning";
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            foreach(FileInfo file in files)
            {
                Console.WriteLine(file.Name);
            }
            


            //Console.WriteLine($"The biggest number is: {Max(numbers)}");
            Console.ReadLine();
        }
    }
}
