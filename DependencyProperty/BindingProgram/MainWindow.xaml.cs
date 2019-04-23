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

namespace BindingProgram
{
    public class Student : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if(PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Student stu;
        List<string> strs;

        public MainWindow()
        {
            InitializeComponent();
            stu = new Student() { Name = "Tom" };
            
            strs = new List<string>() { "Tom", "Tim" };

            //txtboxName.SetBinding(
            //    TextBox.TextProperty,
            //    new Binding("Name")
            //    { Source = stu, Mode = BindingMode.OneWay }
            //    );
            gridRoot.DataContext = stu;
            //txtboxName.SetBinding(TextBox.TextProperty, new Binding("Name"));
        }

        private void BtnChangeName_Click(object sender, RoutedEventArgs e)
        {
            stu.Name += "Tom";
            //strs.Add("ZJU");
        }
    }
}
