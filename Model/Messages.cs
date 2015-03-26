using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public sealed class Messages : Request
    {
        public int Count { get; set; }

        public int Start { get; set; }
         public Messages()
        {

        }

         public Messages(String _url, String _path)
        {
            Url = _url;
            Path = _path;
            Count = 10;
            Start = 0;
        }
    }
}
