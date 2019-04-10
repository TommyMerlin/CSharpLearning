// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/10 14:30:08
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class ProtocalHelper
    {
        private XmlNode fileNode;
        public ProtocalHelper(string protocal)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(protocal);
            fileNode = doc.SelectSingleNode("//file");
        }

        public FileRequestMode GetRequestMode()
        {
            string mode = fileNode.Attributes["mode"].Value;
            mode = mode.ToLower();
            if (mode == "send")
            {
                return FileRequestMode.Send;
            }
            else
            {
                return FileRequestMode.Receive;
            }
        }

        public FileProtocol GetProtocal()
        {
            FileRequestMode mode = GetRequestMode();
            int port = Convert.ToInt32(fileNode.Attributes["port"].Value);
            string fileName = fileNode.Attributes["name"].Value;

            FileProtocol protocol = new FileProtocol(mode, port, fileName);
            return protocol;
        }
    }
}
