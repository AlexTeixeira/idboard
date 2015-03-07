using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Messages : Request
    {
         public Messages()
        {

        }

         public Messages(String _url, String _path)
        {
            Url = _url;
            Path = _path;
            //ApiToCall = this;
        }
    }
}
