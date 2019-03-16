﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
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
    }
}
