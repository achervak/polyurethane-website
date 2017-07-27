using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Data.Entities
{

    [Table("DetailParams")]
    public class ParameterEntity : BaseEntity
    {
        public Guid DetailId { get; set; }
        public string Value { get; set; }

        public virtual DetailEntity Detail { get; set; }
        public virtual ParamGroupEntity Group { get; set; }
    }
}
