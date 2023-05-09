using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMT.EmailService
{
    public class Message
    {
        //public List<MailboxAddress> To { get; set; }
        //public string Subject { get; set; }
        //public string Content { get; set; }

        //public IFormFileCollection Attachments { get; set; }

        //public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        //{
        //    To = new List<MailboxAddress>();

        //    To.AddRange(to.Select(x => new MailboxAddress(x)));
        //    Subject = subject;
        //    Content = content;
        //    Attachments = attachments;
        //}

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments, string email, string password)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
            Email = email;
            Password = password;
        }
    }
}
