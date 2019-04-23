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

    public class 
}
