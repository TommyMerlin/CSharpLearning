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

namespace MyFirstWPF
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

        private void Buttton1_Click(object sender, RoutedEventArgs e)
        {
            Window1 wd1 = new Window1();
            wd1.Show();
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            if (TBVisi.Visibility == Visibility.Hidden)
            {
                TBVisi.Visibility = Visibility.Visible;
            }
            else
            {
                TBVisi.Visibility = Visibility.Hidden;
            }
        }

        private void OpenNewWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            BTNNewWindow.Content = "单击!";
        }

        private void BTNNewWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            BTNNewWindow.Content = "打开一个新窗口";

            //List<int> lis1 = new List<int>();
            //for(int i = 0; i < 10; i++)
            //{
            //    lis1.Add(i);
            //}
            //foreach(int i in lis1)
            //{
            //    MessageBox.Show(i.ToString());
            //}

            //int[] nums = new int[3];
            //nums[0] = 1;
            //nums[1] = 2;
            //nums[2] = 3;
            //foreach (int i in nums)
            //{
            //    MessageBox.Show(i.ToString());
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            //w1.Show();
        }
    }
}
