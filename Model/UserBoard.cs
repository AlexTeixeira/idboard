using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserBoard
    {
        public UserBoard()
        {

        }

        public UserBoard(String _idBoard, String pass)
        {
            IDBoard = _idBoard;
            Password = pass;
        }
        public String IDBoard { get; set; }
        public String Password { get; set; }
        public void Connect(String _url, String _path)
        {
            Request req = new Request(_url, this,_path);
            
        }

        /*public String Name { get; set; }
        public String FirstName { get; set; }*/
    }
}
