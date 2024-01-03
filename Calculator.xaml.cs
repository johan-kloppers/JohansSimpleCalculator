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

        private void UpdateDisplay()
        {
            string CalculationText = string.Empty;

            foreach (object element in OpperationList)
            {
                if (element.GetType() == typeof(ValueItem))
                {
                    CalculationText += ((ValueItem)element).GetValueString();
                }
                else
                {
                    if (element.GetType() == typeof(ManipulationOperationType))
                    {
                        switch (((ManipulationOperationType)element)) {
                            case ManipulationOperationType.Add:
                                CalculationText += "+";
                                break;
                            case ManipulationOperationType.Subtract:
                                CalculationText += "-";
                                break;
                            case ManipulationOperationType.Multiply:
                                CalculationText += "×";
                                break;
                            case ManipulationOperationType.Divide:
                                CalculationText += "÷";
                                break;
                        }

                    }
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
                    else
                    {
                        LastItem = new ValueItem();
                        OpperationList.Add(LastItem);

                        ((ValueItem)LastItem).AddDigit(ButtonValue[0]);
                    }
                }
            }

            UpdateDisplay();
        }

        private void OpperationButtonClick(object sender, RoutedEventArgs e)
        {
            ShouldCalculateResult();

            string? ButtonValue = ((Button)sender).Tag as string;

            if (ButtonValue is not null)
            {
                ManipulationOperationType? type = null;

                switch (ButtonValue)
                {
                    case "+":
                        type = ManipulationOperationType.Add;
                        break;
                    case "−":
                        type = ManipulationOperationType.Subtract;
                        break;
                    case "×":
                        type = ManipulationOperationType.Multiply;
                        break;                    
                    case "÷":
                        type = ManipulationOperationType.Divide;
                        break;
                    case "=":
                        type = ManipulationOperationType.Equals;
                        break;
                }


                object? LastItem = OpperationList.LastOrDefault();

                if (LastItem is not null)
                {
                    if (LastItem.GetType() == typeof(ManipulationOperationType))
                    {
                        if (type is not null)
                        {
                            OpperationList.RemoveAt(OpperationList.Count - 1);
                        }
                    }
                }

                
                if (type is not null)
                {
                    if (type != ManipulationOperationType.Equals)
                    {
                        OpperationList.Add(type);
                    }
                    else
                    {
                        ShouldCalculateResult();
                    }
                }
            }


            UpdateDisplay();
        }

        private void ShouldCalculateResult()
        {
            if (OpperationList.Count == 3)
            {
                if (OpperationList[0].GetType() == typeof(ValueItem))
                {
                    if (OpperationList[1].GetType() == typeof(ManipulationOperationType))
                    {
                        if (OpperationList[2].GetType() == typeof(ValueItem))
                        {
                            switch ((ManipulationOperationType)OpperationList[1])
                            {
                                case ManipulationOperationType.Add:
                                    Add((ValueItem)OpperationList[0], (ValueItem)OpperationList[2]);
                                    break;
                                case ManipulationOperationType.Subtract:
                                    Subtract((ValueItem)OpperationList[0], (ValueItem)OpperationList[2]);
                                    break;
                                case ManipulationOperationType.Multiply:
                                    Multiply((ValueItem)OpperationList[0], (ValueItem)OpperationList[2]);
                                    break;
                                case ManipulationOperationType.Divide:
                                    Divide((ValueItem)OpperationList[0], (ValueItem)OpperationList[2]);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void Divide(ValueItem valueItem1, ValueItem valueItem2)
        {
            OpperationList.Clear();
            ValueItem Result = new ValueItem();

            if (valueItem1.GetValue() == 0 || valueItem2.GetValue() == 0)
            {
                Result.SetValue(0);
            }
            else
            { 
                Result.SetValue(valueItem1.GetValue() / valueItem2.GetValue()); 
            }
            OpperationList.Add(Result);
        }

        private void Multiply(ValueItem valueItem1, ValueItem valueItem2)
        {
            OpperationList.Clear();
            ValueItem Result = new ValueItem();
            Result.SetValue(valueItem1.GetValue() * valueItem2.GetValue());
            OpperationList.Add(Result);
        }

        private void Subtract(ValueItem valueItem1, ValueItem valueItem2)
        {
            OpperationList.Clear();
            ValueItem Result = new ValueItem();
            Result.SetValue(valueItem1.GetValue() - valueItem2.GetValue());
            OpperationList.Add(Result);
        }

        private void Add(ValueItem valueItem1, ValueItem valueItem2)
        {
            OpperationList.Clear();
            ValueItem Result = new ValueItem();
            Result.SetValue(valueItem1.GetValue() + valueItem2.GetValue());
            OpperationList.Add(Result);
        }
    }
}