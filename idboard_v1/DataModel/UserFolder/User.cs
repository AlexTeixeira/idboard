using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idboard_v1.DataModel.UserFolder
{
    public class User
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public List<Sites> Sites { get; set; }
    }
}
