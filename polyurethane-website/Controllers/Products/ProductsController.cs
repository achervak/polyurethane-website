using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using polyurethane_website.Mappers;
using polyurethane_website.Models;
using Polyurethane.Data.Interfaces;

namespace polyurethane_website.Controllers.Products
{
    public class ProductsController : Controller
    {
        private readonly IDataProvider _dataProvider;

        public ProductsController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet]
        [Route("management/detail/show/{guid}")]
        public async Task<ActionResult> Detail(Guid guid)
        {
            var dbDetail = await _dataProvider.GetDetail(guid);
            if (dbDetail == null)
                Redirect("notfound");

            var model = new DetailMapper().Map(dbDetail);
            return View(model);
        }
    }
}