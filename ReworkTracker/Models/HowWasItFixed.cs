using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReworkTracker.Models
{
    public class HowWasItFixed
    {
        public int idHowWasItFixed { get; set; }
        public string HowCode { get; set; }
        public string HowDepartment { get; set; }
        public int HowActive { get; set; }
        public string HowCategory { get; set; }
    }
}
