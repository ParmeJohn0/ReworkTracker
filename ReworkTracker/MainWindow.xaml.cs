
using ReworkTracker.Models;
using ReworkTracker.Services;
using System.ComponentModel;
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
            FillEmployeeComboBox();
            DepartmentComboFill();
            DefectComboFill();
            QtyCombo.Items.Add("1");
            QtyCombo.Items.Add("2");
            QtyCombo.Items.Add("3");
            QtyCombo.Items.Add("4");
            QtyCombo.Items.Add("5");
            QtyCombo.Items.Add("6");
            QtyCombo.Items.Add("7");
            QtyCombo.Items.Add("8");
            QtyCombo.Items.Add("9");
            QtyCombo.Items.Add("10");
            TypeCombo.Items.Add("Scrap");
            TypeCombo.Items.Add("Rework");
            TypeCombo.Items.Add("Waste");

        }
        public event PropertyChangedEventHandler PropertyChanged;
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
            //Insert the data into the database using the comboboxes and textboxes
            Waste waste = new Waste();
            //DateTimeNow
            waste.entry_date = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            //Department
            waste.department_id = DepartmentCombo.Text;
            //Employee
            waste.employee_id = 1;
            //Job Number
            waste.job_number = txtJobNumber.Text;
            //Part Quantity
            waste.part_qty = int.Parse(QtyCombo.Text);
            //Defect Code
            waste.defect_code_id = 1;
            //Issue Description
            waste.issue_description = txtDescription.Text;
            //Improvement Suggestion
            waste.improvement_suggestion = txtDescriptionFixed.Text;
            //Scrap/Rework/Waste
            waste.scrap_rework_waste = TypeCombo.Text;


        }
        //Function to fill the employee combobox
        void FillEmployeeComboBox()
        {
            //Return the values from SQLService 
            SQL_Service sqlService = new SQL_Service();
            sqlService.RetrieveActiveEmployees();
            foreach(Employees employee in sqlService.RetrieveActiveEmployees())
            {
                //Add the employee to the combobox
                EmployeeCombo.Items.Add(employee.FirstName + " " + employee.LastName);
            }
        }
        void DepartmentComboFill()
        {
            //Return the values from SQLService
            SQL_Service sqlService = new SQL_Service();
            sqlService.RetrieveActiveDepartments();

            foreach(Departments department in sqlService.RetrieveActiveDepartments())
            {
                //Add the department to the combobox
                DepartmentCombo.Items.Add(department.Name);
            }
        }
        void DefectComboFill()
        {
            //Return the values from SQLService
            SQL_Service sqlService = new SQL_Service();
            sqlService.RetrieveActiveCodes();

            foreach (Codes codes in sqlService.RetrieveActiveCodes())
            {
                //Add the department to the combobox
                DefectCombo.Items.Add(codes.Code);
            }
        }
    }
}