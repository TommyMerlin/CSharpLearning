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

namespace Practice2._1
{
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void BtnComfirm_Click(object sender, RoutedEventArgs e)
        {
            if(txtboxUserName.Text == "root" && pwboxPassword.Password == "zjuhuye")
            {
                MainWindow mainwin = new MainWindow();
                mainwin.Show();
                Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
