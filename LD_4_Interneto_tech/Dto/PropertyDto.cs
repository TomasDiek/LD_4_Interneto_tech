using System;

namespace LD_4_Interneto_tech.Dto
{
    public class PropertyDto
    {
        public int SellRent { get; set; }
        public int PropertyTypeId { get; set; }
        public int FurnishingTypeId { get; set; }
        public int Price { get; set; }
        public int BuiltArea { get; set; }
        public int CityId { get; set; }
        public bool ReadyToMove { get; set; }
        public string Address { get; set; }
        public int TotalFloors { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; } = 0;
        public string Description { get; set; } 
    }
}