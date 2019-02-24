using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "文本文件|*.txt|PNG图形|*.png";
            ofd1.ShowDialog();
            string fileName = ofd1.FileName;
            txtboxfileName.Text = fileName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.Filter = "文本文件|*.txt|PNG图形|*.png";
            if (sfd1.ShowDialog() == true)
            {
                MessageBox.Show(sfd1.FileName);
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void BtnChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG图片|*.png|JPG图片|*.jpg";
            if (ofd.ShowDialog() == true)
            {
                ImageShow.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
