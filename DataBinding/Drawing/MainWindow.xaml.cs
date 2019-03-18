using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //roTrans.Angle = 90;
            treeitem1.Focus();
            
        }

        private void TreeitemPage_GotFocus(object sender, RoutedEventArgs e)
        {
            frame.Source = new Uri(((TreeViewItem)sender).Tag.ToString(), UriKind.Relative);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rabtn = (RadioButton)sender;
            switch (rabtn.Content)
            {
                case "OP1":
                    MessageBox.Show("OP1!");
                    break;
                case "OP2":
                    MessageBox.Show("OP2!");
                    break;
                case "OP3":
                    MessageBox.Show("OP3!");
                    break;
                case "OP4":
                    MessageBox.Show("OP4!");
                    break;
                default:
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.FontWeight = FontWeights.Bold;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FontWeight = FontWeights.Normal;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.FontStyle = FontStyles.Italic;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            this.FontStyle = FontStyles.Normal;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
