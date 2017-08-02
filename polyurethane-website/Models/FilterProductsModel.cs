using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace polyurethane_website.Models
{
    public class FilterProductsModel
    {
        public string DetailName { get; set; }
        public string DetailDescription { get; set; }
        public string CarModel { get; set; }
        public string CarMake { get; set; }
        public int CarYear { get; set; }
    }
}