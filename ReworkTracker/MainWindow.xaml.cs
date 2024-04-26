using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging.Abstractions;
using ReworkTracker.Models;
using ReworkTracker.Services;
using System.ComponentModel;
using System.Configuration;
using System.Data.Odbc;
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
            DefectComboFill();
            InitialDepartmentFill();
            WhatWasWrongComboFill();
            HowWasItFixedComboFill();
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
            QtyCombo.Items.Add("11");
            QtyCombo.Items.Add("12");
            QtyCombo.Items.Add("13");
            QtyCombo.Items.Add("14");
            QtyCombo.Items.Add("15");
            QtyCombo.Items.Add("16");
            QtyCombo.Items.Add("17");
            QtyCombo.Items.Add("18");
            QtyCombo.Items.Add("19");
            QtyCombo.Items.Add("20");
            TypeCombo.Items.Add("Repair");
            TypeCombo.Items.Add("Replace");
            //user has to input 7 characters into the job number field
            txtJobNumber.MaxLength = 7;
            JobNumberError.Visibility = Visibility.Hidden;
            DefectRequired.Visibility = Visibility.Hidden;
            EmployeeRequired.Visibility = Visibility.Hidden;
            QtyRequired.Visibility = Visibility.Hidden;
            TypeRequired.Visibility = Visibility.Hidden;
            HowRequired.Visibility = Visibility.Hidden;
            LocationHighlight.Visibility = Visibility.Hidden;
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
            txtJobNumber.Text = "";
            QtyCombo.Text = "";
            TypeCombo.Text = "";
            DefectCombo.Text = "";
            HowWasItFixedCombo.SelectedIndex = -1;
            JobNumberError.Visibility = Visibility.Hidden;
            DefectRequired.Visibility = Visibility.Hidden;
            EmployeeRequired.Visibility = Visibility.Hidden;
            QtyRequired.Visibility = Visibility.Hidden;
            TypeRequired.Visibility = Visibility.Hidden;
            HowRequired.Visibility = Visibility.Hidden;
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
            if (string.IsNullOrEmpty(HowWasItFixedCombo.Text))
            {
                HowRequired.Visibility = Visibility.Visible;
            }
            else
            {
                HowRequired.Visibility = Visibility.Hidden;
            }

            //If all the fields are filled out, submit the data to the database
            if (string.IsNullOrEmpty(txtJobNumber.Text)
            || string.IsNullOrEmpty(QtyCombo.Text) || string.IsNullOrEmpty(DefectCombo.Text)
            || string.IsNullOrEmpty(TypeCombo.Text) || string.IsNullOrEmpty(HowWasItFixedCombo.Text))
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
                //foreach (ReportingArea department in sqlService.RetrieveActiveWarsawDepartments())
                //{
                //    if (string.Equals(department.Name, ReportingAreaCombo.Text))
                //    {
                //        waste.department_id = department.iddepartments;
                //    }

                //}
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
                //Resolution Description
                waste.resolution_description = HowWasItFixedCombo.Text;
                //Scrap/Rework/Waste
                waste.scrap_rework_waste = TypeCombo.Text;
                //Insert the data into the database

                sqlService.UpdateWasteDatabase(waste);
                //Clear the form
                ClearButton_Click(sender, e);
            }

        }
        //DS - At start
        void InitialDepartmentFill()
        {
            ReportingAreaCombo.Items.Add("Choose Location First");
        }
        void ReportingAreaComboFill()
        {
            //Add the departments to the combobox
            ReportingAreaCombo.Items.Add("Assembly");
            ReportingAreaCombo.Items.Add("Beamsaw");
            ReportingAreaCombo.Items.Add("Boring and Shaping");
            ReportingAreaCombo.Items.Add("CNC 5 Axis");
            ReportingAreaCombo.Items.Add("CNC Door");
            ReportingAreaCombo.Items.Add("CNC Jamb");
            ReportingAreaCombo.Items.Add("Customs");
            ReportingAreaCombo.Items.Add("Mill");
            ReportingAreaCombo.Items.Add("Prefinish");
            ReportingAreaCombo.Items.Add("Prehang");
            ReportingAreaCombo.Items.Add("Sanding");
            ReportingAreaCombo.Items.Add("SDL/TDL");
            ReportingAreaCombo.Items.Add("Shipping");
            ReportingAreaCombo.Items.Add("Stavecore");
            ReportingAreaCombo.Items.Add("Veneer");

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
        void WhatWasWrongComboFill()
        {
            ////Return the values from SQLService
            //SQL_Service sqlService = new SQL_Service();
            //sqlService.RetrieveWhatWasWrong();

            //foreach (WhatWasWrong whatWasWrong in sqlService.RetrieveWhatWasWrong())
            //{
            //    //Add the department to the combobox
            //    WhatWasWrongCombo.Items.Add(whatWasWrong.WhatCode);
            //}
        }
        void HowWasItFixedComboFill()
        {
            //Retrieve the values from SQLService
            SQL_Service sqlService = new SQL_Service();
            sqlService.RetrieveHowWasItFixed();

            foreach (HowWasItFixed howWasItFixed in sqlService.RetrieveHowWasItFixed())
            {
                //Add the department to the combobox
                HowWasItFixedCombo.Items.Add(howWasItFixed.HowCode);
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
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }
        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if typerequired is visible, set it to hidden.
            if (TypeRequired.Visibility == Visibility.Visible)
            {
                TypeRequired.Visibility = Visibility.Hidden;
            }
        }
        private void QtyCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if typerequired is visible, set it to hidden.
            if (TypeRequired.Visibility == Visibility.Visible)
            {
                TypeRequired.Visibility = Visibility.Hidden;
            }
        }
        private void DefectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if defectrequired is visible, set it to hidden.
            if (DefectRequired.Visibility == Visibility.Visible)
            {
                DefectRequired.Visibility = Visibility.Hidden;
            }
        }
        private void ReportingAreaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ReportingAreaCombo.SelectedItem != null) && (ReportingAreaCombo.SelectedItem.ToString() == "Choose Location First"))
            {
                LocationHighlight.Visibility = Visibility.Visible;
            }
            else
            {
                LocationHighlight.Visibility = Visibility.Hidden;
            }
        }
        public List<WhatWasWrong> RetrieveWhatWasWrong()
        {
            List<WhatWasWrong> objReturn = new List<WhatWasWrong>();
            WhatWasWrong whatWasWrong;
            string strSQLcall = string.Empty;

            //Set SQL statement, add a where clause that checks the dep_cateogry for the department selected in the ReportingAreaCombo.
            // Add the department that they selected at the end of the SQL statement.
            strSQLcall = "SELECT what_was_wrong.what_code FROM what_was_wrong INNER JOIN departments ON what_was_wrong.dep_category = departments.category where departments.Name = ";
            //Add the department that they selected at the end of the SQL statement. Set that value to the department selected in the ReportingAreaCombo.
            strSQLcall = strSQLcall + "'" + ReportingAreaCombo.SelectedItem.ToString() + "' and Facility = ";
            //If the WarsawRadioButton is checked, set the facility to Warsaw. If the CastileRadioButton is checked, set the facility to Castile.
            if (WarsawRadio.IsChecked == true)
            {
                strSQLcall = strSQLcall + "'Warsaw'";
            }
            else if (CastileRadio.IsChecked == true)
            {
                strSQLcall = strSQLcall + "'Castile'";
            }

            try
            {
                using (OdbcConnection conn = new OdbcConnection(ConfigurationManager.AppSettings.Get("WasteConnectionString")))
                {
                    conn.Open();
                    OdbcCommand cmd = new OdbcCommand(strSQLcall);
                    cmd.Connection = conn;
                    using OdbcDataReader odbcDataReader = cmd.ExecuteReader();
                    while (odbcDataReader.Read())
                    {
                        whatWasWrong = new WhatWasWrong();
                        whatWasWrong.WhatCode = odbcDataReader.GetString(0);
                        objReturn.Add(whatWasWrong);
                    }
                }
            }
            catch (Exception ex)
            {
                // logentry = "\n •Error retrieving WhatWasWrong from DB" + ex.Message + timestamp;
                // System.IO.File.AppendAllText(logfilepath, logentry);
            }
            return objReturn;
        }
        private void EmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if employeerequired is visible, set it to hidden.
            if (EmployeeRequired.Visibility == Visibility.Visible)
            {
                EmployeeRequired.Visibility = Visibility.Hidden;
            }
        }
        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void txtDescriptionFixed_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if howrequired is visible set it to hidden.
            if (HowRequired.Visibility == Visibility.Visible)
            {
                HowRequired.Visibility = Visibility.Hidden;
            }
        }
        private void WarsawRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ReportingAreaCombo.Items.Clear();
            ReportingAreaComboFill();
            HowWasItFixedCombo.Items.Clear();

        }
        private void CastileRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            ReportingAreaCombo.Items.Clear();
            ReportingAreaComboFill();
            HowWasItFixedCombo.Items.Clear();
        }
    }
}