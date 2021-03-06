﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Data.Entities
{
    [Table("ImageInDetail")]
    public class ImageInDetailEntity : BaseEntity
    {
        public Guid DetailId { get; set; }
        public Guid ImageId { get; set; }
        public virtual ImageEntity Image { get; set; }
    }
}
