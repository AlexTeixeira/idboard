using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.classes
{
    public class User
    {
        private static readonly User user = new User();
        public static String IdBoard { get; set; }
        public static String Password { get; set; }
        private User() { }

        public static User getUser
        {
            get
            {
                return user;
            }
        }

        public static void setArguments(String _idboard, String _password)
        {
            IdBoard = _idboard;
            Password = _password;
        }
    }
}
