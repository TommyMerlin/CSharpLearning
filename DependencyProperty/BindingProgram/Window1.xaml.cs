using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BindingProgram
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            Binding binding = new Binding("Value") { Source = slider1 };
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            RangeValidationRule vdr = new RangeValidationRule();
            vdr.ValidatesOnTargetUpdated = true;
            binding.ValidationRules.Add(vdr);
            binding.NotifyOnValidationError = true;
            txtbox1.SetBinding(TextBox.TextProperty, binding);
            txtbox1.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(ValidationErrorHandler));
        }

        private void ValidationErrorHandler(object sender, RoutedEventArgs e)
        {
            if(Validation.GetErrors(txtbox1).Count >= 1)
            {
                MessageBox.Show(Validation.GetErrors(txtbox1)[0].ErrorContent.ToString());
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            List<Browser> bs = new List<Browser>()
            {
                new Browser() { name = "Chrome", cate=Category.Chrome, state=State.Avaliable},
                new Browser() { name = "Edge", cate=Category.Edge, state=State.Unknown},
                new Browser() { name = "Chrome", cate=Category.Chrome, state=State.Locked},
                new Browser() { name = "Edge", cate=Category.Edge, state=State.Locked},
                new Browser() { name = "Chrome", cate=Category.Chrome, state=State.Unknown},
                new Browser() { name = "Edge", cate=Category.Edge, state=State.Avaliable},
                new Browser() { name = "Chrome", cate=Category.Chrome, state=State.Unknown},
                new Browser() { name = "Chrome", cate=Category.Chrome, state=State.Avaliable},
            };
            this.lbBrowser.ItemsSource = bs;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class RangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double d;
            if(double.TryParse(value.ToString(),out d))
            {
                if (d >= 0 && d <= 100)
                {
                    return new ValidationResult(true, null);
                }
            }

            return new ValidationResult(false, "Validation Falied");
        }
    }

    public enum Category
    {
        Chrome,
        Edge
    }

    public enum State
    {
        Avaliable,
        Locked,
        Unknown
    }

    public class Browser
    {
        public Category cate { get; set; }
        public State state { get; set; }
        public string name { get; set; }
    }

    public class Category2SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Category c = (Category)value;
            switch (c)
            {
                case Category.Chrome:
                    return @"Images/chrome.png";
                case Category.Edge:
                    return @"Images/edge.png";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class State2NullableBoolConverer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State s = (State)value;
            switch (s)
            {
                case State.Avaliable:
                    return true;
                case State.Locked:
                    return false;
                case State.Unknown:
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nb = (bool?)value;
            switch (nb)
            {
                case false:
                    return State.Locked;
                case true:
                    return State.Avaliable;
                case null:
                default:
                    return State.Unknown;
            }
        }
    }
}
