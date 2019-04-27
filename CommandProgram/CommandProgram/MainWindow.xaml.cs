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

namespace CommandProgram
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCommand();
        }

        private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));

        public void InitializeCommand()
        {
            this.btn1.Command = this.clearCmd;
            this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            this.btn1.CommandTarget = this.txtbox1;

            CommandBinding cbd = new CommandBinding();
            cbd.Command = this.clearCmd;
            cbd.CanExecute += Cbd_CanExecute;
            cbd.Executed += Cbd_Executed;
            this.stackPanel.CommandBindings.Add(cbd);
        }

        private void Cbd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.txtbox1.Clear();
            e.Handled = true;
        }

        private void Cbd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbox1.Text)) { e.CanExecute = false; }
            else { e.CanExecute = true; }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbox1.Text)) { e.CanExecute = false; }
            else { e.CanExecute = true; }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string par = e.Parameter.ToString();
            switch (par)
            {
                case "Student":
                    lbName.Items.Add($"New Student: {txtbox1.Text}");
                    break;
                case "Teacher":
                    lbName.Items.Add($"New Teacher: {txtbox1.Text}");
                    break;
                default:
                    break;
            }
        }
    }
}
