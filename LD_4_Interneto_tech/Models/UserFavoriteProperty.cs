using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LD_4_Interneto_tech.Models
{
    public class UserFavoriteProperty: BaseEntity
    {
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Property")]
        public int? PropertyId { get; set; }
        
        public User User { get; set; }
        
        public Property Property { get; set; }
    }
}
