using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.classes
{
    public class Login
    {
        public String IDBoard { get; set; }
        public String Password { get; set; }

        private static Login instance;

        private Login() { }

        public static Login Instance
       {
          get 
          {
             if (instance == null)
             {
                 instance = new Login();
             }
             return instance;
          }
       }


    }
}
