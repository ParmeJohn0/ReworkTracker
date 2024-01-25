using Microsoft.Extensions.Logging.Abstractions;
using Mysqlx.Crud;
using ReworkTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReworkTracker.Services
{
    public class SQL_Service
    {
        /// <summary>
        /// DS - 1/25/24
        /// Returns active employees from the employees table in the upstate_service database
        /// </summary>
        /// <returns> List<Employees> </returns>
        public List<Employees> RetrieveActiveEmployees() 
        {             
            List<Employees> objReturn = new List<Employees>();
            Employees employee;
            string strSQLcall = string.Empty;

            //Set SQL statement
            strSQLcall = "SELECT idEmployees, FirstName, LastName, Type FROM upstate_service.employees WHERE Active = 1";

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
                        employee = new Employees();
                        employee.idEmployees = odbcDataReader.GetInt32(0);
                        employee.FirstName = odbcDataReader.GetString(1);
                        employee.LastName = odbcDataReader.GetString(2);
                        employee.Type = odbcDataReader.GetString(3);
                        objReturn.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                //logentry = "\n •Error retrieving Packid from Pack table: " + ex.Message;
                //System.IO.File.AppendAllText(logfilepath, logentry);
            }
            return objReturn;               
        }

        /// <summary>
        /// DS - 1/25/24
        /// Returns active departments from the departments table in the upstate_service database
        /// </summary>
        /// <returns> List<Departments> </returns>
        public List<Departments> RetrieveActiveDepartments()
        {
            List<Departments> objReturn = new List<Departments>();
            Departments department;
            string strSQLcall = string.Empty;

            //Set SQL statement
            strSQLcall = "SELECT iddepartments, Name, Facility, type FROM upstate_service.departments WHERE Active = 1";

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
                        department = new Departments();
                        department.iddepartments = odbcDataReader.GetInt32(0);
                        department.Name = odbcDataReader.GetString(1);
                        department.Facility = odbcDataReader.GetString(2);
                        department.type = odbcDataReader.GetString(3);
                        objReturn.Add(department);
                    }                    
                }
            }
            catch (Exception ex)
            {
                //logentry = "\n •Error retrieving Packid from Pack table: " + ex.Message;
                //System.IO.File.AppendAllText(logfilepath, logentry);
            }
            return objReturn;
        }

        /// <summary>
        /// DS - 1/25/24
        /// Returns active defect codes from the defect_codes table in the upstate_service database
        /// </summary>
        /// <returns> List<Codes> </returns>
        public List<Codes> RetrieveActiveCodes()
        {
            List<Codes> objReturn = new List<Codes>();
            Codes code;
            string strSQLcall = string.Empty;

            //Set SQL statement
            strSQLcall = "SELECT iddefect_codes, Code, Grouper FROM upstate_service.defect_codes WHERE Active = 1";

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
                        code = new Codes();
                        code.iddefect_codes = odbcDataReader.GetInt32(0);
                        code.Code = odbcDataReader.GetString(1);
                        if(!(odbcDataReader.IsDBNull(2)))
                        {
                            code.Group = odbcDataReader.GetString(2);
                        }
                        else
                        {
                            code.Group = "No Group";
                        }

                        objReturn.Add(code);
                    }
                }
            }
            catch (Exception ex)
            {
                //logentry = "\n •Error retrieving Packid from Pack table: " + ex.Message;
                //System.IO.File.AppendAllText(logfilepath, logentry);
            }
            return objReturn;
        }
        /// <summary>
        /// DS - 1/25/24
        /// Update the waste_database table with the supplied Waste entry
        /// </summary>
        /// <returns> bUpdated </returns>
        public bool UpdateWasteDatabase(Waste objWaste)
        {
            bool bUpdated = false;
            string strSQLcall = string.Empty;
          
            try
            {
                using (OdbcConnection cn = new OdbcConnection(ConfigurationManager.AppSettings.Get("WasteConnectionString")))
                {
                    cn.Open();                    
                    {
                        var command = cn.CreateCommand();
                        command.CommandText = "INSERT INTO waste_database (entry_date, department_id, employee_id, job_number, part_qty, defect_code_id, " +
                            "issue_description, improvement_suggestion, scrap_rework_waste) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?);";
                        command.Parameters.AddWithValue("$entry_date", objWaste.entry_date);
                        command.Parameters.AddWithValue("$department_id", objWaste.department_id);
                        command.Parameters.AddWithValue("$employee_id", objWaste.employee_id);
                        command.Parameters.AddWithValue("$job_number", objWaste.job_number);
                        command.Parameters.AddWithValue("$part_qty", objWaste.part_qty);
                        command.Parameters.AddWithValue("$defect_code_id", objWaste.defect_code_id);
                        command.Parameters.AddWithValue("$issue_description", objWaste.issue_description);
                        command.Parameters.AddWithValue("$improvement_suggestion", objWaste.improvement_suggestion);
                        command.Parameters.AddWithValue("$scrap_rework_waste", objWaste.scrap_rework_waste);

                        command.ExecuteNonQuery();
                    }
                    bUpdated = true;
                }
            }
            catch (Exception ex)
            {
                //Show scanerror message
                //scan_error scan_Error = new scan_error();
                //scan_Error.ShowDialog();
                //logentry = "\n •Error inserting records into Tally table: " + ex.Message;
                //System.IO.File.AppendAllText(logfilepath, logentry);
            }
            return bUpdated;
        }
    }
}
