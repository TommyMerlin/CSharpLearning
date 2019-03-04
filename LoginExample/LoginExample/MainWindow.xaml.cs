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

namespace LoginExample
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

        // 取消登陆
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // 确认登陆
        private void BtnComfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = txtboxUserName.Text.Trim();
                string userPwd = pwboxPassword.Password;
                Model.UserInfo userInfo = new Model.UserInfo(userName, userPwd);

                BLL.LoginManager loginManager = new BLL.LoginManager();
                if (loginManager.LoginComfirm(userInfo))
                {
                    MessageBox.Show("登陆用户: " + userName);
                }
                else
                {
                    MessageBox.Show("登陆失败");
                    txtboxUserName.Text = "";
                    pwboxPassword.Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
