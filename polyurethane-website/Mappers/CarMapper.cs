using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using polyurethane_website.Models;
using Polyurethane.Data.Entities;

namespace polyurethane_website.Mappers
{
    public class CarMapper
    {
        public CarModel Map(CarEntity car)
        {
            var model = new CarModel()
            { 
                Model = car.Model,
                Guid = car.Id,
                Make = car.Make,
                Description = car.Description,
                EndYear = car.YearEnd,
                StartYear = car.YearStart
            };

            return model;
        }
    }
}