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

namespace Drawing
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            List<Unit> infos = new List<Unit>()
            {
                new Unit(){Year = "2014",Price = 176},
                new Unit(){Year = "2015",Price = 150},
                new Unit(){Year = "2016",Price = 300},
                new Unit(){Year = "2017",Price = 286},
                new Unit(){Year = "2018",Price = 100},
                new Unit(){Year = "2019",Price = 200}
            };
            lb1.ItemsSource = infos;
            //this.Cursor = Cursors.SizeAll;
        }
    }
}
