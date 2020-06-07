using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data
{
    public class ChattyDbContext : IdentityDbContext
    {
        public ChattyDbContext(DbContextOptions<ChattyDbContext> options)
            : base(options)
        {
        }
    }
}
