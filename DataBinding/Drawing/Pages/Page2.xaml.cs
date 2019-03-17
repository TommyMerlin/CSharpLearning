using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Drawing.Pages
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            List<Student> students = new List<Student>()
            {
                new Student(){Name = "HZ",Age = 200},
                new Student(){Name = "NB",Age = 100},
            };
            lbInfo.ItemsSource = students;
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            rec1.Fill = Brushes.AliceBlue;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
