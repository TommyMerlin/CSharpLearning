using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat src = new Mat(@"F:\desktop\browse10.png", ImreadModes.Grayscale);
            // Mat src = Cv2.ImRead("lenna.png", ImreadModes.Grayscale);
            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}
