using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Data.Entities
{
    [Table("Car")]
    public class CarEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public int YearStart { get; set; }
        [Required]
        public int YearEnd { get; set; }

        public virtual ICollection<DetailEntity> Details { get; set; } = new HashSet<DetailEntity>();
    }
}
