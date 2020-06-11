using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatty.Data;
using Chatty.Models;
using Chatty.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Controllers
{
    public class ChatController : Controller
        {
        private readonly ChattyDbContext _context;

        public ChatController(ChattyDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost] 
        public JsonResult GetAllContacts()
        {
            var contacts = _context.Users.Where(u => u.UserName != User.Identity.Name).ToList();
            var contactList = new List<Dictionary<string, object>>();
            foreach (var contact in contacts)
            {
                contactList.Add(new Dictionary<string, object>(){
                    ["Name"] = contact.UserName
                });
            }
            return this.Json(contactList);
        }
        [Authorize]
        [HttpPost] 
        public JsonResult GetMessages([FromBody] mContact messageGetRequest)
        {
            var userName = messageGetRequest.UserName;
            var user = _context.Users.Where(u => u.UserName == userName).SingleOrDefault();
            var currentUser = _context.Users
                .Where(u => u.UserName == User.Identity.Name)
                .Include(u => u.SentMessages)
                    .ThenInclude(m => m.Receiver)
                .Include(u => u.ReceivedMessages)
                    .ThenInclude(m => m.Sender)
                .SingleOrDefault();
            var sentMessages = currentUser.SentMessages.Where(sm => sm.Receiver == user);
            var receivedMessages = currentUser.ReceivedMessages.Where(rm => rm.Sender == user);
            var messages = new List<mMessage>();
            foreach (var message in sentMessages)
            {
                messages.Add(
                    new mMessage(message, "sent")
                );
            }
            foreach (var message in receivedMessages)
            {
                messages.Add(
                    new mMessage(message, "received")
                );
            }
            var orderedMessages = messages.OrderBy(m => m.SentOn);
            var returnPayload = new { PublicKey=user.PublicKey, Messages=orderedMessages};

            return this.Json(returnPayload);
        }
        [Authorize]
        [HttpPost] 
        public JsonResult SendMessage([FromBody] mNewMessage newMessage)
        {
            var receiver = _context.Users.Where(u => u.UserName == newMessage.Receiver).SingleOrDefault();
            var currentUser = _context.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();

            var message = new Message
            {
                Content = newMessage.Content,
                Sender = currentUser,
                Receiver = receiver,
                SentIn = null,
            };

            _context.Add(message);
            _context.SaveChanges();
            return this.Json(new Dictionary<string, string>(){["message"] = "Message Delivered successfully."});
        }
        [Authorize]
        [HttpPost] 
        public JsonResult UpdatePublicKey([FromBody] mGeneratedKey generatedKey)
        {
            var currentUser = _context.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();

            currentUser.PublicKey = generatedKey.PublicKey;

            _context.SaveChanges();

            return this.Json(new Dictionary<string, string>(){["message"] = "Key updated succesfully."});
        }
    }
}