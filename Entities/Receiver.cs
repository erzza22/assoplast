using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Entities
{
    public class Receiver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public string DestinationLocation { get; set; }
        [Required]
        public string AuthorizationNumber { get; set; }
        [Required]
        public DateTime AuthorizationDate { get; set; }
        [Required]
        public int ReceiverCategoryId { get; set; }
        public DestinatarioCategory DestinatarioCategory { get; set; }
    }
}
