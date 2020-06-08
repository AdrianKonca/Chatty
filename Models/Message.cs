using System;
using System.ComponentModel.DataAnnotations;

namespace Chatty.Models
{
    public class Message
    {
        public int Id { get; private set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public User Sender { get; set; }
        [Required]
        public User Receiver { get; set; }
        public Group SentIn { get; set; }
        [Required]
        public DateTime SentOn { get; private set; }
    }
}