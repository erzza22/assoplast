using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Entities
{
    public class ProduttoreDetentore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string LocalUnit { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public int ProducerCategoryId { get; set; }



    }
}
