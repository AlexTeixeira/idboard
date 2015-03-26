using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Model.CoursesFolder
{
    public sealed class Course
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Teacher { get; set; }
        public string Comments { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string BackColor { get; set; }
        public string BarColor { get; set; }
        public string FontColor { get; set; }
    }
}
