using BackgroundTask.Model.MessageFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Model.CoursesFolder
{
    public sealed class CoursesInfo
    {
        public Result Result { get; set; }

        public IEnumerable<Course> Courses { get; set; }

    }
}
