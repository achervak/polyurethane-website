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
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int YearStart { get; set; }
        [Required]
        public int YearEnd { get; set; }
    }
}
