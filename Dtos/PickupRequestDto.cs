using System;

namespace MVC.Dtos
{
    public class PickupRequestDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime PickupDate { get; set; }
        public int NumberOfObjects { get; set; }
        public string Note { get; set; }
    }
}
