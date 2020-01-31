using System;

namespace MVC.Dtos
{
    public class ProduttoreDetentoreDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string LocalUnit { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public int ProducerCategoryId { get; set; }
        public string ProducerCategoryName { get; set; }
    }
}
