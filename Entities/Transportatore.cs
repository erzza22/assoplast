using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Entities
{
    public class Transportatore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string EstablishmentName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AuthorizationNumber { get; set; }
        [Required]
        public DateTime AuthorizationDate { get; set; }

        [Required]
        public int TransportatoreCategoryId { get; set; }
        public TransportatoreCategory TransportatoreCategory { get; set; }
    }
}
