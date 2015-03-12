using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idboard_v1.DataModel.UserFolder;

namespace idboard_v1.DataModel.MessageFolder
{
    public class MessagesInfo
    {
        public Result Result { get; set; }
        public List<MyMessages> Messages { get; set; }
    }
}
