using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int Age { get; set; }
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Student stu;
        List<string> strs;
        //List<Student> stuList;
        ObservableCollection<Student> StuList;

        public MainWindow()
        {
            InitializeComponent();

            StuList = new ObservableCollection<Student>()
            {
                new Student(){Name = "Tom", Age=12, Id=10001},
                new Student(){Name = "Amy", Age=21, Id=10002},
                new Student(){Name = "Sheldon", Age=22, Id=10003},
                new Student(){Name = "Lenerd", Age=23, Id=10004},
                new Student(){Name = "Howard", Age=24, Id=10005},
                new Student(){Name = "Penny", Age=25, Id=10006},
                new Student(){Name = "Raj", Age=26, Id=10007}
            };

            lbStudents.ItemsSource = StuList;
            //lbStudents.DisplayMemberPath = "Name";

            txtboxStudentId.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = this.lbStudents });
        }

        int StuId = 10008;
        private void BtnChangeName_Click(object sender, RoutedEventArgs e)
        {
            //stu.Name += "Tom";
            //strs.Add("ZJU");
            StuList.Add(new Student() { Name = "May", Age = 12, Id = StuId });
            StuId++;
        }
    }
}
