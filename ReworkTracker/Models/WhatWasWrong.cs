using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReworkTracker.Models
{
    public class WhatWasWrong
    {
        public int idWhatWasWrong { get; set; }
        public string WhatCode { get; set; }
        public string WhatDepartment { get; set; }
        public int WhatActive { get; set; }
        public string WhatCategory { get; set; }    
    }
}
