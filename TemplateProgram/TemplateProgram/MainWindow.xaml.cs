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

namespace TemplateProgram
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Product> products = new List<Product>()
            {
                new Product(128,"2016"),
                new Product(80,"2017"),
                new Product(150,"2018"),
                new Product(200,"2019"),
            };
            lbox.ItemsSource = products;
        }
    }

    public class Product
    {
        public Product(int price, string year)
        {
            Price = price;
            Year = year ?? throw new ArgumentNullException(nameof(year));
        }

        public int Price { get; set; }
        public string Year { get; set; }
    }
}
