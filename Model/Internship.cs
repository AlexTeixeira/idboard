using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Internship : Request
    {
        public int IDInternship { get; set; }

         public Internship(String _url, String _path)
        {
            Url = _url;
            Path = _path;
        }
    }
}
