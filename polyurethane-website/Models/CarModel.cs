using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polyurethane_website.Models
{
    public class CarModel
    {
        public Guid Guid { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int StartYear { get; set; }
        public string Description { get; set; }
        public int EndYear { get; set; }
    }
}