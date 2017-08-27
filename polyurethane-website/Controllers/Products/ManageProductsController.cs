using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using polyurethane_website.Mappers;
using polyurethane_website.Models;
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
        
        [Route("management/car/full-list-by-detail/{datailGuid}")]
        [Route("management/car/full-list-by-detail/")]
        public async Task<ViewResult> GetFullCarListByDetail(Guid? datailGuid)
        {
            if (!datailGuid.HasValue)
                datailGuid = Guid.Empty;
                    
            var carList = await _dataProvider.GetCars();
            var mapper = new CarMapper();
            var model = from car in carList
                select new SuitabilityCarResult()
                { 
                    Car = mapper.Map(car),
                    IsSuitable = car.Details.Any( x => x.Id == datailGuid)
                };

            return View(model);
        }

        [HttpGet]
        [Route("management/detail/create")]
        public ViewResult CreateNewDetail()
        {
            return View();
        }
        
        [HttpPost]
        [Route("management/detail/{guid}/add-image/")]
        public async Task AddDetailToImage(Guid guid)
        {
            var dbDetail = await _dataProvider.GetDetail(guid);
            if (dbDetail == null)
                throw new Exception("No detail were found");

            if (Request.Files.Count > 0)
            {
                var imageUrl = $"/images/details/{guid}";
                var imagesFolder = Server.MapPath(imageUrl);
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                for (var i = 0; i < Request.Files.Count; i++)
                {
                    try
                    {
                        var image = Request.Files[i];
                        var imageGuid = Guid.NewGuid();
                        //--create image object
                        var dbImage = new ImageEntity()
                        {
                            Name = image.FileName,
                            Url = $"{imageUrl}/{imageGuid}{Path.GetExtension(image.FileName)}"
                        };

                        image.SaveAs($"{imagesFolder}/{imageGuid}{Path.GetExtension(image.FileName)}");
                        //-- link image to datail
                        await _dataProvider.AddImageToDetail(dbDetail, dbImage);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return;
        }

        [HttpPost]
        [Route("management/detail/create")]
        public async Task<ActionResult> CreateNewDetail(UpdateDetailModel detail)
        {
            var dbDetail = await _dataProvider.CreateDetail(new DetailEntity()
            {
                Name = detail.DetailName,
                ShortDescription = detail.ShortDescription,
                Description = detail.Description,
                Price = detail.Price
            });
            
            if (dbDetail.Id == Guid.Empty)
                Redirect("management/detail/create");

            await _dataProvider.SetDetailParams(dbDetail, detail.Params);
            await _dataProvider.SetDetailCars(dbDetail, detail.Cars);

            return Json(new { Id = dbDetail.Id });
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