using System;

namespace Chatty.Models.DTO
{
    public class mMessage
    {
        public string Content { get; set; }
        public DateTime SentOn { get; set; }
        public string Type { get; set; }
        public mMessage(Message message, string type)
        {
            this.Content = message.Content;
            this.SentOn = message.SentOn;
            this.Type = type;
        }
    }
}