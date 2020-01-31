using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Entities
{
    public class EmailAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AttachmentName { get; set; }
        public byte[] AttachmentContent { get; set; }
        public string AttachmentMimeType { get; set; }

        [Required]
        public int EmailId { get; set; }
        public Email Email { get; set; }
    }
}
