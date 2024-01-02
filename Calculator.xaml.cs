using JohansSimpleCalculator.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JohansSimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        List<object> OpperationList = new List<object>(); 

        public Calculator()
        {
            InitializeComponent();
        }

        private void titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void WindowControls_CloseWindow(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowControls_MaximizeWindow(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            WindowBorder.Margin = new Thickness(5);
        }

        private void WindowControls_UnMaximizeWindow(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
            WindowBorder.Margin = new Thickness(0);
        }

        private void WindowControls_MinimizeWindow(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            string? ButtonValue = ((Button)sender).Tag as string;

            if (ButtonValue is not null)
            {
                bool result = Int32.TryParse(ButtonValue, out int NumberValue);

                if (result is not false)
                {
                    object? LastItem = OpperationList.LastOrDefault();

                    if (LastItem is null)
                    {
                        LastItem = new ValueItem();
                        OpperationList.Add(LastItem);
                    }

                    if (LastItem.GetType() == typeof(ValueItem))
                    {
                        ((ValueItem)LastItem).AddDigit(ButtonValue[0]);
                    }
                }
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            string CalculationText = string.Empty;

            foreach (object element in OpperationList)
            {
                if (element.GetType() == typeof(ValueItem))
                {
                    CalculationText += ((ValueItem)element).GetValueString();
                }
            }

            CalculationTxt.Text = CalculationText;

            List<object> ReversedList = OpperationList.ToList();
            ReversedList.Reverse();
            foreach (object element in ReversedList)
            {
                if (element.GetType() == typeof(ValueItem))
                {
                    NumberTxt.Text = ((ValueItem)element).GetValueString();
                    break;
                }
            }
            
        }
    }
}