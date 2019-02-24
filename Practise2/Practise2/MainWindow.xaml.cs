using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Practise2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Student stu;
        public MainWindow()
        {
            InitializeComponent();
            txtbox1.SetBinding(TextBlock.TextProperty, new Binding("Name") { Source = stu = new Student() });
            Binding binding = new Binding() { Source=slider1,Path=new PropertyPath("Value")};
            txtblock1.SetBinding(TextBox.TextProperty, binding);
            txtblock2.SetBinding(TextBlock.TextProperty, new Binding("Text.Length") { Source = txtblock1 });
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee(){Id=12345,Name="John",Age=11},
                new Employee(){Id=23456,Name="Mary",Age=12},
                new Employee(){Id=34567,Name="Sheldon",Age=13},
                new Employee(){Id=45678,Name="Penny",Age=14},
            };
            this.listboxEmployeeList.ItemsSource = employeeList;
            this.listboxEmployeeList.DisplayMemberPath = "Name";
            this.txtboxID.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = listboxEmployeeList});
        }

        class Student : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string name;

            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (checkboxgraduated.IsChecked == true)
            {
                MessageBox.Show("You're graduated!");
            }
            else
            {
                MessageBox.Show("You're still at school!");
            }
        }

        private void Pubtn3_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxinput.Text != "")
            {
                CheckBox cb = new CheckBox
                {
                    Content = txtboxinput.Text
                };
                listbox1.Items.Add(cb);
            }
        }

        
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            stu.Name += "Name ";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void link_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }

        private void Run_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            poplink.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Users\\7ommy\\Desktop";
            ofd.Filter = "txt文件|*.txt|PNG图片|*.png|所有文件|*.*";
            if (ofd.ShowDialog() == true)
            {
                txtboxpath.Text = ofd.FileName;
            }
        }
    }
    public class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
    }
}
