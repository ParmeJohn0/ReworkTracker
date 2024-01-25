using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReworkTracker.Models
{
    public class Waste
    {
        public int entry_date { get; set; }
        public string department_id { get; set; }
        public int employee_id { get; set; }
        public string job_number { get; set; }
        public int part_qty { get; set; }
        public int defect_code_id { get; set; }
        public string issue_description { get; set; }
        public string improvement_suggestion { get; set; }
        public string scrap_rework_waste { get; set; }



    }
}
