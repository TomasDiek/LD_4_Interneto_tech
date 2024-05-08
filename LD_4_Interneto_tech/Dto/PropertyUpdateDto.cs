namespace LD_4_Interneto_tech.Dto
{
    public class PropertyUpdateDto
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Address { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyType { get; set; }
        public int FurnishingTypeId { get; set; }
        public string FurnishingType { get; set; }
        public int Price { get; set; }
        public int BuiltArea { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool ReadyToMove { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int TotalFloors { get; set; }
        public int Age { get; set; } = 0;
        public string Description { get; set; }

    }
}
