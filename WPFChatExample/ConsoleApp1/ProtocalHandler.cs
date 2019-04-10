// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/10 14:29:42
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ProtocalHandler
    {
        private string PartialProtocal;

        public ProtocalHandler()
        {
            PartialProtocal = string.Empty;
        }

        public string[] GetProtocal(string input)
        {
            return GetProtocal(input, null);
        }

        private string[] GetProtocal(string input,List<string> outputList)
        {
            if(outputList == null)
            {
                outputList = new List<string>();
            }

            if (!string.IsNullOrEmpty(PartialProtocal))
            {
                input = PartialProtocal + input;
            }

            string pattern = "(^<protocol>.*?</protocol>)";

            if (Regex.IsMatch(input, pattern))
            {
                string match = Regex.Match(input, pattern).Groups[0].Value;
                outputList.Add(match);
                PartialProtocal = string.Empty;

                input = input.Substring(match.Length);

                GetProtocal(input, outputList);
            }
            else
            {
                PartialProtocal = input;
            }

            return outputList.ToArray();

        }
    }

    public enum FileRequestMode
    {
        Send = 0,
        Receive
    }

    public struct FileProtocol
    {
        private readonly FileRequestMode mode;
        private readonly int port;
        private readonly string fileName;

        public FileProtocol
            (FileRequestMode mode, int port, string fileName)
        {
            this.mode = mode;
            this.port = port;
            this.fileName = fileName;
        }

        public FileRequestMode Mode
        {
            get { return mode; }
        }

        public int Port
        {
            get { return port; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public override string ToString()
        {
            return String.Format("<protocol><file name=\"{0}\" mode=\"{1}\" port=\"{2}\" /></protocol>", fileName, mode, port);
        }
    }
}
