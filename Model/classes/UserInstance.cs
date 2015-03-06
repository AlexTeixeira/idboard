using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.classes
{
    public class UserInstance
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String RoleName { get; set; }

        private static UserInstance instance;

         private UserInstance() { }

         public static UserInstance Instance
       {
          get 
          {
              if (instance == null)
             {
                 instance = new UserInstance();
             }
              return instance;
          }
       }
    }
}
