using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;

namespace Polyurethane.Data.Entities
{
    public class TemplatesEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public TemplateType TemplateType { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
    }
}
