using System;

namespace LD_4_Interneto_tech.Dto
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public int sellRent {get; set;}
        public string Address { get; set;}
        public string PropertyType { get; set; }
        public string FurnishingType { get; set; }
        public int Price { get; set; }
        public int BuiltArea { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool ReadyToMove { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public string Photo { get; set; }
    }
}