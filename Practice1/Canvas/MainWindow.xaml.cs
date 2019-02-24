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

namespace Canvas
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
            listbox1.Items.Add(textbox1.Text);
            textbox1.Text = "";
        }

        private void Listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textbox1.Text = listbox1.SelectedItem.ToString();
            txtblock1.FontFamily = new FontFamily(listbox1.SelectedItem.ToString());
            //txtblock1.FontSize = 100;
            
        }

        Random rnd = new Random();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_add.Background = new SolidColorBrush(Colors.AliceBlue);
            btn_add.Foreground = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            foreach(FontFamily font in Fonts.SystemFontFamilies)
            {
                listbox1.Items.Add(font.Source);
            }
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int Text = (int)slider1.Value;
            textbox2.Text = Text.ToString();
        }

        private void Textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int num = int.Parse(textbox2.Text);
                slider1.Value = num;
            }
            catch
            {
                slider1.Value = 0;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.ShowDialog();
            
        }

        public class Human
        {
            public string name;
        }
    }
}
