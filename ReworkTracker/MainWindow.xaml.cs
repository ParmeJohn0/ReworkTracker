using ReworkTracker.Models;
using ReworkTracker.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            //user has to input 7 characters into the job number field
            txtJobNumber.MaxLength = 7;
            JobNumberError.Visibility = Visibility.Hidden;
            DefectRequired.Visibility = Visibility.Hidden;
            DepartmentRequired.Visibility = Visibility.Hidden;
            EmployeeRequired.Visibility = Visibility.Hidden;
            QtyRequired.Visibility = Visibility.Hidden;
            TypeRequired.Visibility = Visibility.Hidden;
            DescriptionRequired.Visibility = Visibility.Hidden;
            HowRequired.Visibility = Visibility.Hidden;
            DescriptionRequired.Visibility = Visibility.Hidden;
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
            //if (WindowState != System.Windows.WindowState.Normal)
            //{
            //    WindowState = System.Windows.WindowState.Normal;
            //    if (WindowState == WindowState.Normal)
            //    {

            //    }
            //}
            //else
            //{
            //    WindowState = WindowState.Maximized;
            //    if (WindowState == WindowState.Maximized)
            //    {
            //    }
            //}
        }
        void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
        void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //clear all the fields in the form
            txtJobNumber.Text = "";
            txtDescription.Text = "";
            txtDescriptionFixed.Text = "";
            QtyCombo.Text = "";
            TypeCombo.Text = "";
            DepartmentCombo.Text = "";
            EmployeeCombo.Text = "";
            DefectCombo.Text = "";
            txtImprovement.Text = "";
            JobNumberError.Visibility = Visibility.Hidden;
            DefectRequired.Visibility = Visibility.Hidden;
            DepartmentRequired.Visibility = Visibility.Hidden;
            EmployeeRequired.Visibility = Visibility.Hidden;
            QtyRequired.Visibility = Visibility.Hidden;
            TypeRequired.Visibility = Visibility.Hidden;
            DescriptionRequired.Visibility = Visibility.Hidden;
            HowRequired.Visibility = Visibility.Hidden;
            DescriptionRequired.Visibility = Visibility.Hidden;
        }
        void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Show the error messages if the fields are empty
            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                JobNumberError.Visibility = Visibility.Visible;
            }
            else
            {
                JobNumberError.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(DepartmentCombo.Text))
            {
                DepartmentRequired.Visibility = Visibility.Visible;
            }
            else
            {
                DepartmentRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(EmployeeCombo.Text))
            {
                EmployeeRequired.Visibility = Visibility.Visible;
            }
            else
            {
                EmployeeRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(QtyCombo.Text))
            {
                QtyRequired.Visibility = Visibility.Visible;
            }
            else
            {
                QtyRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(DefectCombo.Text))
            {
                DefectRequired.Visibility = Visibility.Visible;
            }
            else
            {
                DefectRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(TypeCombo.Text))
            {
                TypeRequired.Visibility = Visibility.Visible;
            }
            else
            {
                TypeRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                DescriptionRequired.Visibility = Visibility.Visible;
            }
            else
            {
                DescriptionRequired.Visibility = Visibility.Hidden;
            }
            if (string.IsNullOrEmpty(txtImprovement.Text))
            {
                HowRequired.Visibility = Visibility.Visible;
            }
            else
            {
                HowRequired.Visibility = Visibility.Hidden;
            }
            //If all the fields are filled out, submit the data to the database
            if (string.IsNullOrEmpty(txtJobNumber.Text) || string.IsNullOrEmpty(DepartmentCombo.Text) || string.IsNullOrEmpty(EmployeeCombo.Text) || string.IsNullOrEmpty(QtyCombo.Text) || string.IsNullOrEmpty(DefectCombo.Text) || string.IsNullOrEmpty(TypeCombo.Text) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtImprovement.Text))
            {
                //Do nothing
            }
            else
            {
                //Insert the data into the database using the comboboxes and textboxes
                Waste waste = new Waste();
                SQL_Service sqlService = new SQL_Service();
                //DateTimeNow
                waste.entry_date = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                //Department
                foreach (Departments department in sqlService.RetrieveActiveDepartments())
                {
                    if (string.Equals(department.Name, DepartmentCombo.Text))
                    {
                        waste.department_id = department.iddepartments;
                    }

                }
                //Employee
                foreach (Employees employee in sqlService.RetrieveActiveEmployees())
                {
                    if (string.Equals(employee.FirstName + " " + employee.LastName, EmployeeCombo.Text))
                    {
                        waste.employee_id = employee.idEmployees;
                    }

                }
                //Job Number
                waste.job_number = txtJobNumber.Text;
                //Part Quantity
                waste.part_qty = int.Parse(QtyCombo.Text);
                //Defect Code
                foreach (Codes codes in sqlService.RetrieveActiveCodes())
                {
                    if (string.Equals(codes.Code, DefectCombo.Text))
                    {
                        waste.defect_code_id = codes.iddefect_codes;
                    }

                }
                //Issue Description
                waste.issue_description = txtDescription.Text;
                //Improvement Suggestion
                waste.improvement_suggestion = txtImprovement.Text;
                //Resolution Description
                waste.resolution_description = txtDescriptionFixed.Text;
                //Scrap/Rework/Waste
                waste.scrap_rework_waste = TypeCombo.Text;
                //Insert the data into the database

                sqlService.UpdateWasteDatabase(waste);
                //Clear the form
                ClearButton_Click(sender, e);
            }

        }
        //Function to fill the employee combobox
        void FillEmployeeComboBox()
        {
            //Return the values from SQLService
            SQL_Service sqlService = new SQL_Service();
            sqlService.RetrieveActiveEmployees();
            foreach (Employees employee in sqlService.RetrieveActiveEmployees())
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

            foreach (Departments department in sqlService.RetrieveActiveDepartments())
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
        private void txtJobNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check to see if theres 7 characters, if not , disable the submit button
            if (txtJobNumber.Text.Length == 7)
            {
                JobNumberError.Visibility = Visibility.Hidden;
                SubmitButton.IsEnabled = true;
            }
            else
            {
                //Messagebox telling user to input 7 characters
                JobNumberError.Visibility = Visibility.Visible;
                SubmitButton.IsEnabled = false;
            }
        }
    }
}