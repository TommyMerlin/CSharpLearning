using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Panuon.UI;
using AutoCAD;
using Microsoft.Win32;
using System.Xml;
using System.Data;

namespace DataBinding
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : PUWindow
    {
        
        /// <summary>
        /// 窗口1
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            TCPListener listener = new TCPListener();
            
            //bind command
            
        }

        void bin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.Source as Button;
            this.frame1.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            frame1.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
        }

        private void PUButton_Click(object sender, RoutedEventArgs e)
        {
            PUMessageBox.ShowDialog("Hello");
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filepath = ofd.FileName;
            AcadApplication app = new AcadApplication
            {
                Visible = true
            };
            AcadDocument doc = app.Documents.Open(filepath, null, null);
            
        }
    }
}
