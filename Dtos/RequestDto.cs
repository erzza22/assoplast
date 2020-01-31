using MVC.Enums;
using System;


namespace MVC.Dtos
{
    public class RequestDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Notes { get; set; }
          
        public int TransporterId { get; set; }
        public int ProducerId { get; set; }
        public int ReceiverId { get; set; }
        public int RequestCategoryId { get; set; }
        public string RequestCategoryName { get; set; }

        public string CharacteristicsDescription { get; set; }
        public string CharacteristicsCode { get; set; }
        public string CharacteristicsState { get; set; }
        public string CharacteristicsDangerDescription { get; set; }

        public int CharacteristicsNumberOfContainers { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string DestinationTypeDescription { get; set; }
        public string DestinationPhysicalChemicalProprieties { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
    }
}
