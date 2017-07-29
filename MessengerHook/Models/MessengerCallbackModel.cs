using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerHook.Models
{
    public class MessengerCallbackModel
    {
        public string Object { get; set; }
        public IEnumerable<MessengerEventModel> Entry { get; set; }
    }
}
