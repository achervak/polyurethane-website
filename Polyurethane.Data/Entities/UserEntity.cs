using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Data.Entities
{
    [Table("Customers")]
    public class UserEntity : BaseEntity
    {
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(500)]
        public string Details { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();

    }
}
