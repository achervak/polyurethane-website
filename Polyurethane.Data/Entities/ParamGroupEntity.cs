using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Data.Entities
{
    [Table("ParamGroup")]
    public class ParamGroupEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsFilter { get; set; }

        public virtual ICollection<ParameterEntity> Parameters { get; set; } = new HashSet<ParameterEntity>();

    }
}
