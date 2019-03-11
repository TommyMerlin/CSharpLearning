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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace FileOperation
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

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();

            DirectoryInfo info = new DirectoryInfo(fbd.SelectedPath);

            FileSystemInfo[] fileSystemInfos = info.GetFileSystemInfos();

            foreach(FileSystemInfo inf in fileSystemInfos)
            {
                lbFileName.Items.Add(inf.FullName);
            }
            txtboxPath.Text = fbd.SelectedPath;
        }
    }
}
