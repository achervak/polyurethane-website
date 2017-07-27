using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;

namespace Polyurethane.Data.Models
{
    public class Detail
    {
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        public List<DetailParamModel> Params { get; set; } = new List<DetailParamModel>();
    }
}
