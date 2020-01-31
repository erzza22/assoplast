using MVC.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC.Entities
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Notes { get; set; }

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

        [Required]
        public int RequestCategoryId { get; set; }
        public RequestCategory RequestCategory { get; set; }
        [Required]
        public int TransporterId { get; set; }
        public Transportatore Transportatore { get; set; }
        [Required]
        public int ProducerId { get; set; }
        public ProduttoreDetentore ProduttoreDetentore { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }
    }
}
