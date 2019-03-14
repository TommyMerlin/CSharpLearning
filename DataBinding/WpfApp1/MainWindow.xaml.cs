using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
            //Process.GetProcesses();
            //Process.Start("notepad");
            //Process.Start("iexplore", "https://www.baidu.com");

            
        }

        List<string> pathList = new List<string>();

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP3文件|*.mp3|所有文件|*.*";
            ofd.Title = "Please choose your music file(s)";
            ofd.InitialDirectory = @"D:\CloudMusic\Download";
            ofd.Multiselect = true;
            ofd.ShowDialog();
            string[] path;
            path = ofd.FileNames;
            for(int i=0; i<path.Length; i++)
            {
                lbMusic.Items.Add(Path.GetFileName(path[i]));
                pathList.Add(path[i]);
            }

            //lbMusic.ItemsSource = ofd.FileNames;
            
        }

        private MediaElement media = new MediaElement();
        private MediaPlayer player = new MediaPlayer();

        private void LbMusic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            // media.LoadedBehavior = MediaState.Manual;
            player.Open(new Uri(pathList[lbMusic.SelectedIndex]));
           // media.Source = new Uri(lbMusic.SelectedItem.ToString());
            player.Play();
        }

        /// <summary>
        /// 下一曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            int index = lbMusic.SelectedIndex;
            index++;
            if(index == lbMusic.Items.Count)
            {
                index = 0;
            }
            lbMusic.SelectedIndex = index;
            player.Open(new Uri(pathList[index]));
            player.Play();
        }

        /// <summary>
        /// 上一曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            int index = lbMusic.SelectedIndex;
            index--;
            if (index == 0)
            {
                index = lbMusic.Items.Count - 1;
            }
            player.Open(new Uri(pathList[index]));
            player.Play();
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Thread th = new Thread(PlayGame);
            th.IsBackground = true;
            th.Start();
        }

        public void PlayGame()
        {
            Random rnd = new Random();
            while (true)
            {
                label1.Content = rnd.Next(0, 10).ToString();
                label2.Content = rnd.Next(0, 10).ToString();
                label3.Content = rnd.Next(0, 10).ToString();
            }
            
        }

        public void Test()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}

