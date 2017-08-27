using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;

namespace Polyurethane.Data.Entities
{
    public class ConfigurationEntiy : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Value { get; set; }
        public ConfigurationType ConfigurationType { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }
}
