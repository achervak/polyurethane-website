using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using polyurethane_website.Mappers;
using polyurethane_website.Models;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.DataManagers;
using Polyurethane.Data.Models.Orders;

namespace polyurethane_website.Controllers.Products
{
    public class ProductsController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IOrderDataManager _orderDataManager;
        private readonly IMapper _mapper;

        public ProductsController(IDataProvider dataProvider, IOrderDataManager orderDataManager, IMapper mapper)
        {
            _orderDataManager = orderDataManager;
            _dataProvider = dataProvider;
            _mapper = mapper;
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
        
        [Route("param/groups")]
        public async Task<ActionResult> GetParamGroups(string filter)
        {
            var groups = await _dataProvider.GetParamGroups(filter);
            return Json(groups.Select(x => new { Name = x.Name, IsFilter = x.IsFilter }), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        [Route("products/list/")]
        public async Task<ActionResult> GetProductList(FilterProductsModel filters)
        {
            Expression<Func<DetailEntity, bool>> filter = (detail) =>
                filters.DetailName == null || detail.Name.Contains(filters.DetailName)
                && filters.DetailDescription == null || detail.Description.Contains(filters.DetailDescription);

            var model = new DetailViewModel
            {
                TotalFilteredCount = await _dataProvider.GetDetailsCount(filter)
            };

            var dbDetails = await _dataProvider.GetDetails(filter, 20, 1);
            model.Details = dbDetails.Select(x => new DetailMapper().Map(x));

            return View(model);
        }
        /// <summary>
        /// Search details by some string
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Route("products/search/list")]
        public async Task<ActionResult> GetSearchProductList(string filter)
        {
            Expression<Func<DetailEntity, bool>> filterAction = (detail) =>
                detail.Name.Contains(filter) ||
                detail.Description.Contains(filter);

            var model = new DetailViewModel();
            model.TotalFilteredCount = await _dataProvider.GetDetailsCount(filterAction);

            var dbDetails = await _dataProvider.GetDetails(filterAction, 20, 1);
            model.Details = dbDetails.Select(x => new DetailMapper().Map(x));

            return View("GetProductList", model);
        }

        /// <summary>
        /// Search detail by string container
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Route("products/search")]
        public ViewResult SearchProducts(string filter)
        {

            var model = new SearchProducts()
            {
                Filter = filter
            };

            return View(model);
        }



    }
}