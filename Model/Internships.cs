using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Internships : Request
    {
        public int Count { get; set; }

        public int Start { get; set; }
         public Internships()
        {

        }

         public Internships(String _url, String _path)
        {
            Url = _url;
            Path = _path;
            Count = 20;
            Start = 0;
            //ApiToCall = this;
        }
    }
}
