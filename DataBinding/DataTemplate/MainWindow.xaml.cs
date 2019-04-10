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

namespace DataTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Browser> browsers = new List<Browser>()
            {
                new Browser("browse1"),
                new Browser("browse2"),
                new Browser("browse3"),
                new Browser("browse4"),
                new Browser("browse5"),
                new Browser("browse6"),
                new Browser("browse7"),
                new Browser("browse8"),
                new Browser("browse9"),
                new Browser("browse10"),
                new Browser("browse11"),
                new Browser("browse12")
            };

            lbName.ItemsSource = browsers;
        }
    }

    public class Browser
    {
        public Browser(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; set; }
    }
}
