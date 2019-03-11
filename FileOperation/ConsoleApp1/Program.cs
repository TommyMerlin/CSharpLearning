using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Test
    {
        static void Main(string[] args)
        {
            string path = @"F:\TestDir\MyTest.txt";
            //string target = @"F:\TestDir";

            /**
            try
            {
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("路径不存在");
                    Directory.CreateDirectory(path);
                    Console.WriteLine("路径创建成功");
                    Console.WriteLine("The number of files in {0} is {1}.", path, Directory.GetFiles(path).Length);
                }
                if (Directory.Exists(target))
                {
                    Directory.Delete(target, true);
                }
                Directory.Move(path, target);
                Console.WriteLine("The number of files in {0} is {1}.", target, Directory.GetFiles(target).Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process failed: {0}", ex.Message);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            try
            {
                if (dirInfo.Exists)
                {
                    Console.WriteLine("Directory alredy exists.");
                }
                else
                {
                    dirInfo.Create();
                }
                dirInfo.MoveTo(target);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            try
            {
                if (!File.Exists(path))
                {
                    string content = "Hello World.\n";
                    File.WriteAllText(path, content);
                }
                string[] appendTexts = new string[] { "C# Programming", "Python Programming" };
                string appendText = "C# Programming\n";
                File.AppendAllLines(path,appendTexts);
                File.AppendAllLines(path,appendTexts);
                File.AppendAllText(path, appendText);
                File.AppendAllText(path, appendText);

                string readText = File.ReadAllText(path);
                Console.WriteLine(readText);
            }
            catch
            {
                Console.WriteLine("Error!");
            }

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                Console.WriteLine("File Already Exists.");
            }
            else
            {
                fileInfo.Create();
            }

            string content = "MyTest.";
            string[] appendLines = new string[] { "Python", "C#", "C++" };
            string appendText = "Hello World.";

            Console.WriteLine(fileInfo.Name);
            Console.WriteLine(fileInfo.FullName);
            Console.WriteLine(fileInfo.Length);
            Console.WriteLine(fileInfo.Extension);
            Console.WriteLine(fileInfo.CreationTime);
            Console.WriteLine(fileInfo.LastWriteTime);
            Console.WriteLine(fileInfo.DirectoryName);
            **/

            FileStream s1 = new FileStream(path,FileMode.OpenOrCreate);
            TextWriter tw = new StreamWriter(s1);

            
            //tw.Write("nHello World");
            tw.WriteLine("Hello World");
            tw.Write("Hello ");
            tw.WriteLine("World");
            tw.Write("C# Programming");
            tw.Close();

            FileStream s2 = new FileStream(path, FileMode.OpenOrCreate);
            TextReader tr = new StreamReader(s2);
            Console.WriteLine(tr.ReadToEnd());


            
            Console.Read();
        }
    }
}
