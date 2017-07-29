using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerHook.Models
{
    public class MessengerEventModel
    {
        public string Id { get; set; }
        public int Time { get; set; }
        public IEnumerable<MessagingModel> Messaging { get; set; }
    }

    public class MessagingModel
    {
        public SenderModel Sender { get; set; }
        public ReceipientModel Receipient { get; set; }
        public int Timestamp { get; set; }
        public MessageModel Message { get; set; }
    }

    public class SenderModel
    {
        public string Id { get; set; }
    }

    public class ReceipientModel
    {
        public string Id { get; set; }
    }

    public class MessageModel
    {
        public string Mid { get; set; }
        public string Text { get; set; }
    }
}
