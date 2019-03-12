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

namespace DataBinding
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public static readonly DependencyProperty OpenCommandProperty =
    DependencyProperty.Register("OpenCommand", typeof(RoutedCommand), typeof(Window1), new PropertyMetadata(null));

        public RoutedCommand OpenCommand
        {
            get { return (RoutedCommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }

        public Window1()
        {
            InitializeComponent();
            //bind command
            this.OpenCommand = new RoutedCommand();
            var bin = new CommandBinding(this.OpenCommand);
            bin.Executed += bin_Executed;
            this.CommandBindings.Add(bin);
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
    }
}
