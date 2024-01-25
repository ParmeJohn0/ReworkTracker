using ReworkTracker.Services;
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
using System.Windows.Threading;

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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToLongTimeString();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        public string Time = DateTime.Now.ToString("HH:mm:ss");
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
        void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
        void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //clear all the fields in the form

        }
        void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //call the SQL statement to insert the data into the database

        }
    }
}