using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> StudentList;

        public MainWindow()
        {
            InitializeComponent();
            StudentList = new ObservableCollection<Student>()
            {
                new Student("Tom",12,1001),
                new Student("Jerry",22,1002),
                new Student("Amy",23,1003),
                new Student("Sheldon",32,1004),
                new Student("Tony",45,1005),
            };
            lbox.ItemsSource = StudentList;
            grid1.ItemsSource = StudentList;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            StudentList.Add(new Student("Hugo", 5, 1006));
        }
    }

    public class Student
    {
        public Student(string name, int age, int iD)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
            ID = iD;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public int ID { get; set; }
    }
}
