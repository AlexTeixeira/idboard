using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Model.MessageFolder
{
    public sealed class MessagesInfo
    {
        public Result Result { get; set; }

        public IEnumerable<MyMessages> Messages { get; set; }
    }
}
