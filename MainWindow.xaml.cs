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
    public partial class MainWindow : Window
    {
        public MainWindow()
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
    }
}