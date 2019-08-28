using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Practise1
{
    public class BusTicketSys
    {
        public string[,] Matrix { get; set; }
        
        public BusTicketSys()
        {
            Matrix = new string[9, 4];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Matrix[i, j] = "有票";
                }
            }
        }

        public void ShowMatrix()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0,-6}",Matrix[i,j]);
                }
                Console.WriteLine();
            }
        }

        public void ModifyMatrix(int row,int col)
        {
            Matrix[row, col] = "无票";
        }
    }

    class MyClass : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MyClass()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    class Program
    {
        static void Main(string[] args)
        {

            PdfWriter writer = new PdfWriter("Hello.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document doc = new Document(pdf);
            doc.Add(new Paragraph("Hello World!"));
            doc.Close();



            Console.Read();
        }
    }
}
