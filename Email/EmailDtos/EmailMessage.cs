using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Email.EmailDtos
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public List<byte[]> Attachments { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string MessageId { get; set; }
        public string FileName { get; set; }
        public bool IsRead { get; set; }

        public EmailMessage()
        {
            FromAddresses = new List<EmailAddress>();
            Attachments = new List<byte[]>();
        }
    }
}
