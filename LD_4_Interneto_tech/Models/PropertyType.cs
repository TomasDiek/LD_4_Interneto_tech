using System.ComponentModel.DataAnnotations;

namespace LD_4_Interneto_tech.Models
{
    public class PropertyType :BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}