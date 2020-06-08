using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatty.Models
{
    public class Group
    {
        public int Id { get; private set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; private set; }
        public ICollection<GroupMember> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}