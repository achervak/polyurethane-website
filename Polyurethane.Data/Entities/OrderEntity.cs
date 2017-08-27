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
    public class OrderEntity : BaseEntity
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public UserEntity Customer { get; set; }
        public Guid CustomerId { get; set; }
        public virtual ICollection<DetailEntity> Details { get; set; } = new HashSet<DetailEntity>();
        [MaxLength(1000)]
        public string DeliveryDetails { get; set; }
        [MaxLength(1000)]
        public string OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
