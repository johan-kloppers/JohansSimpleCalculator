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

namespace JohansSimpleCalculator.Controls
{
    /// <summary>
    /// Interaction logic for WindowControls.xaml
    /// </summary>
    public partial class WindowControls : UserControl
    {
        public WindowState State;

        Geometry UnMaximizedSymbol = Geometry.Parse("M 17.58,10.58 C 17.58,10.58 26.44,10.53 26.44,10.53 26.44,10.53 26.49,19.44 26.49,19.44 26.49,19.44 17.51,19.47 17.51,19.47 17.51,19.47 17.58,10.58 17.58,10.58");

        Geometry MaximizedSymbol = Geometry.Parse("M 17.49,14.51 C 17.49,14.51 24.51,14.53 24.51,14.53 24.51,14.53 24.47,21.49 24.47,21.49 24.47,21.49 17.49,21.51 17.49,21.51 17.49,21.51 17.49,14.51 17.49,14.51M 19.51,14.49 C 19.51,14.49 19.53,12.53 19.53,12.53 19.53,12.53 26.47,12.58 26.47,12.58 26.47,12.58 26.49,19.49 26.49,19.49 26.49,19.49 24.49,19.44 24.49,19.44");
        public WindowControls()
        {
            InitializeComponent();
        }

        public event EventHandler CloseWindow;
        public event EventHandler MaximizeWindow;
        public event EventHandler UnMaximizeWindow;
        public event EventHandler MinimizeWindow;

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MinimizeWindow != null)
            {
                MinimizeWindow(this, EventArgs.Empty);
            }
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (State == WindowState.UnMaximized)
            {
                if (MaximizeWindow != null)
                {
                    MaximizeWindow(this, EventArgs.Empty);
                }

                MaximizeSymbol.Data = MaximizedSymbol;

                State = WindowState.Maximized;
            }
            else
            {
                if (UnMaximizeWindow != null)
                {
                    UnMaximizeWindow(this, EventArgs.Empty);
                }

                MaximizeSymbol.Data = UnMaximizedSymbol;

                State = WindowState.UnMaximized;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CloseWindow != null)
            {
                CloseWindow(this, EventArgs.Empty);
            }
        }

        public enum WindowState
        {
            Maximized, UnMaximized
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is not null)
            {
                if (parentWindow.WindowState == System.Windows.WindowState.Normal)
                {
                    State = WindowState.UnMaximized;
                    MaximizeSymbol.Data = UnMaximizedSymbol;
                }
                else if (parentWindow.WindowState == System.Windows.WindowState.Maximized)
                {
                    State = WindowState.Maximized;
                    MaximizeSymbol.Data = MaximizedSymbol;
                }
            }
        }
    }
}
