using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        [Route("management/car/create")]
        public ViewResult CreateNewCar()
        {
            return View("CreateNewCar");
        }

        public async Task<string> Test()
        {
            var car = await _dataProvider.CreateCar(new CarEntity()
            {
                Make = "Ford",
                Model = "Focus",
                YearStart = 2001,
                YearEnd = 2005,
            });

            return car.Id.ToString();
        }
    }
}