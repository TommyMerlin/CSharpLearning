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

namespace DependencyLearning
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
            stu = new Student();
            //Binding binding = new Binding("Text") { Source = txtbox1 };
            //stu.SetBingding(Student.NameProperty, new Binding("Text") { Source = txtbox1 });
            //txtbox2.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = stu });

            //this.gridRoot.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonClicked));
            this.btnOK.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonClicked));
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((e.OriginalSource as FrameworkElement).Name.ToString());
            FrameworkElement element = (FrameworkElement)sender;
            MessageBox.Show(element.Name);
        }

        private void GridRoot_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello");
        }

        private void ReportTimeEventHandler(object sender, ReportTimeEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            string time = e.ClickTime.ToLongTimeString();
            string content = $"{time} 到达 {element.Name}";
            this.lbox.Items.Add(content);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //MessageBox.Show(stu.GetValue(Student.NameProperty).ToString());
        //    //MessageBox.Show(stu.GetValue(Student.AgeProperty).ToString());

        //    //Human human = new Human();
        //    //School.SetGrade(human, 6);
        //    //int grade = School.GetGrade(human);
        //    //MessageBox.Show(grade.ToString());

        //    FrameworkElement element = (FrameworkElement)sender;
        //    MessageBox.Show(element.Name);
        //}
    }
}
