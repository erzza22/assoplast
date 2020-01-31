using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Dtos
{
    public class ReceiverDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string DestinationLocation { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public int ReceiverCategoryId { get; set; }
        public string ReceiverCategoryName { get; set; }
    }
}
