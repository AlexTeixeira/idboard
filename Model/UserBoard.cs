using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserBoard : Request
    {
        public UserBoard()
        {

        }

        public UserBoard(String _url, String _path)
        {
            Url = _url;
            Path = _path;
            //ApiToCall = this;
        }

        /*public String Name { get; set; }
        public String FirstName { get; set; }*/
    }
}
