using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Courses : Request
    {
        public String DateStart { get; set; }
        public String DateEnd { get; set; }

        public Courses(String _url, String _path, String _dateStart, String _dateEnd)
        {
            Url = _url;
            Path = _path;
            DateEnd = _dateEnd;
            DateStart = _dateStart;
        }
    }
}
