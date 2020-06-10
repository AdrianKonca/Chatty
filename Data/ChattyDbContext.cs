using System;
using System.Collections.Generic;
using System.Text;
using Chatty.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data
{
    public class ChattyDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        DbSet<Group> Groups;
        DbSet<Message> Messages;
        DbSet<GroupMember> GroupMembers;
        public ChattyDbContext(DbContextOptions<ChattyDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.SentMessages);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(r => r.ReceivedMessages);

            modelBuilder.Entity<Message>()
                .Property(m => m.SentOn)
                .HasDefaultValueSql("Now()")
                .HasColumnType("DATETIME");

            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.UserId, gm.GroupId });  
            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(gm => gm.UserId);  
            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GroupId);
        }
    }
}
