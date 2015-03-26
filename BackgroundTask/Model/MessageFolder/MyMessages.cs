using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Model.MessageFolder
{
    public sealed class MyMessages
    {
        public string Title { get; set; }
        public string BBCode { get; set; }
        public string DateStart { get; set; }
        public bool IsPriority { get; set; }
    }
}
