using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Email.EmailDtos
{
    public class EmailsDto
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string EmailAddress { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string BodyContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
