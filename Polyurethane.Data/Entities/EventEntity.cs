using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;

namespace Polyurethane.Data.Entities
{
    [Table("Events")]
    public class EventEntity : BaseEntity
    {
        public EventType Event { get; set; }
        public DateTime EventTime { get; set; }
        [MaxLength(100)]
        public string Details { get; set; }
        [MaxLength(1500)]
        public string MoreDetils { get; set; }
        public string LoggedBy { get; set; }
    }
}
