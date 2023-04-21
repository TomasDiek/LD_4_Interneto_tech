using System.Collections.Generic;

namespace LD_4_Interneto_tech.Dto
{
    public class PropertyDetailDto : PropertyListDto
    {
        public string Address { get; set; }
        public int TotalFloors { get; set; }    
        public int Age { get; set; }
        public string Description { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}