using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Dtos
{
    public class TransportatoreDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public int TransportatoreCategoryId { get; set; }
        public string TransportatoreCategoryName { get; set; }
        public string EstablishmentName { get; set; }
    }
}
