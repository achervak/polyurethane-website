using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyurethane.Data.Entities
{
    [Table("Detail")]
    public class DetailEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public virtual ICollection<ImageEntity> Images { get; set; } = new HashSet<ImageEntity>();
        public virtual ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();
        public virtual ICollection<ParameterEntity> Params { get; set; } = new HashSet<ParameterEntity>();
    }
}
