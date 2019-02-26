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

namespace Toolbar
{
    /// <summary>
    /// NewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewWindow : Window
    {
        
        public NewWindow()
        {
            InitializeComponent();
        }
        public string Username { get; set; }
        public  string Password { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Username = txtName.Text;
            Password = pbpwd.Password;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
