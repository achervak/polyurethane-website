using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polyurethane_website.Models
{
    public class DetailViewModel
    {
        public int TotalFilteredCount = 0;
        public IEnumerable<DetailModel> Details { get; set; } = new List<DetailModel>();
    }
}