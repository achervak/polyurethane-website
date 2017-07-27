using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Models;

namespace polyurethane_website.Controllers.Products
{
    public class ManageProductsController : Controller
    {
        private readonly IDataProvider _dataProvider;

        public ManageProductsController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        [HttpGet]
        [Route("management/detail/create")]
        public ViewResult CreateNewDetail()
        {
            return View();
        }

        [HttpPost]
        [Route("management/detail/create")]
        public async Task<ActionResult> CreateNewDetail(DetailEntity detail)
        {
            detail = await _dataProvider.CreateDetail(detail);
            var images = Request.Files;
            if (detail.Id == Guid.Empty)
                Redirect("management/detail/create");

            //-- store detail images
            if (images.Count > 0)
            {
                var imageUrl = $"/images/details/{detail.Id}";
                var imagesFolder = Server.MapPath(imageUrl);
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                for ( var i = 0; i < images.Count; i++)
                {
                    try
                    {
                        var image = images[i];
                        var imageGuid = Guid.NewGuid();
                        //--create image object
                        var dbImage = new ImageEntity()
                        {
                            Name = image.FileName,
                            Url = $"{imageUrl}/{imageGuid}{Path.GetExtension(image.FileName)}"
                        };

                        image.SaveAs($"{imagesFolder}/{imageGuid}{Path.GetExtension(image.FileName)}");
                        //-- link image to datail
                        await _dataProvider.AddImageToDetail(detail, dbImage);
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            
            await _dataProvider.SetDetailParams(detail, new List<DetailParamModel>()
            {
                new DetailParamModel() { Key = "width", Value = "35", IsFilter = false},
                new DetailParamModel() { Key = "height", Value = "20", IsFilter = false},
                new DetailParamModel() { Key = "color", Value = "Blue", IsFilter = true}
            });

            return Redirect($"/management/detail/show/{detail.Id}");
        }

        [HttpGet]
        [Route("management/car/create")]
        public ViewResult CreateNewCar()
        {
            return View("CreateNewCar");
        }

        [HttpPost]
        [Route("management/car/create")]
        public async Task<ActionResult> CreateNewCar(CarEntity car)
        {
            car = await _dataProvider.CreateCar(car);
            return Redirect(car.Id != Guid.Empty ? "/" : "management/car/create");
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