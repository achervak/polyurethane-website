using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Polyurethane.Data.Models;

namespace polyurethane_website.Models
{
    public class UpdateDetailModel
    {
        //public Guid Guid { get; set; }
        public string DetailName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<DetailParamModel> Params { get; set; } = new List<DetailParamModel>();
    }
}