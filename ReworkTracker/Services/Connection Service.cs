using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReworkTracker.Services
{
    public class Connection_Service
    {
        public static List<string> ExecuteQuery(string strQuery, int intCol)
        {
            List<string> strReturn = new List<string>();

            try
            {
                using (OdbcConnection conn = new OdbcConnection(ConfigurationManager.AppSettings.Get("WasteConnectionString")))
                {
                    conn.Open();
                    OdbcCommand cmd = new OdbcCommand(strQuery);
                    cmd.Connection = conn;
                    using OdbcDataReader odbcDataReader = cmd.ExecuteReader();
                    while (odbcDataReader.Read())
                    {
                        switch (intCol)
                        {
                            case 1:
                                strReturn.Add(odbcDataReader.GetString(0));
                                break;
                            case 2:
                                strReturn.Add($"{odbcDataReader.GetString(0)} {odbcDataReader.GetString(1)}");
                                break;
                         }

                        
                    }
                }
            }
            catch (Exception ex)
            {                              
                //logentry = "\n •Error retrieving Packid from Pack table: " + ex.Message;
                //System.IO.File.AppendAllText(logfilepath, logentry);
            }

            return strReturn;
        }
    }
}
