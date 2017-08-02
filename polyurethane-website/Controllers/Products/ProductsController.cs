using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using polyurethane_website.Mappers;
using polyurethane_website.Models;
using Polyurethane.Data.Entities;
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

        [HttpGet]
        [Route("products/view/{guid}")]
        public async Task<ActionResult> GetViewProduct(Guid guid)
        {
            var dbDetail = await _dataProvider.GetDetail(guid);
            if (dbDetail == null)
                Redirect("/products/list/");

            var model = new DetailMapper().Map(dbDetail);
            return View("Detail",model);
        }

        //[HttpPost]
        [Route("products/list/")]
        public async Task<ActionResult> GetProductList(FilterProductsModel filters)
        {
            Expression<Func<DetailEntity, bool>> filter = (detail) =>
                filters.DetailName == null || detail.Name.Contains(filters.DetailName)
                && filters.DetailDescription == null || detail.Description.Contains(filters.DetailDescription);// ||
                                                                                                     //filters.CarMake != null && detail.Cars.FirstOrDefault();

            var model = new DetailViewModel();

            model.TotalFilteredCount = await _dataProvider.GetDetailsCount(filter);

            var dbDetails = await _dataProvider.GetDetails(filter, 20, 1);
            model.Details = dbDetails.Select(x => new DetailMapper().Map(x));

            return View(model);
        }

        [Route("products/search")]
        public async Task<ActionResult> SearchProducts(string filter)
        {

            var model = new SearchProducts()
            {
                Filter = filter
            };

            return View(model);
        }



    }
}