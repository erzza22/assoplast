using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Entities
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string MessageId { get; set; }
        public string EmailAddress { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string BodyContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public ICollection<EmailAttachment> EmailAttachments { get; set; }
    }
}
