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

namespace ReworkTracker
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        void Close_Click(object sender, RoutedEventArgs e) => Close();
        void Min_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        void Max_Click(object sender, RoutedEventArgs e)
        {

            if (WindowState != System.Windows.WindowState.Normal)
            {
                WindowState = System.Windows.WindowState.Normal;
                if (WindowState == WindowState.Normal)
                {

                }
            }
            else
            {
                WindowState = WindowState.Maximized;
                if (WindowState == WindowState.Maximized)
                {


                }
            }
        }
    }
}