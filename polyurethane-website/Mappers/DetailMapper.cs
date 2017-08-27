using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using polyurethane_website.Models;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models;

namespace polyurethane_website.Mappers
{
    public class DetailMapper
    {
        public DetailModel Map(DetailEntity entity)
        {
            var detail = new DetailModel()
            {
                Guid = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ShortDescription = entity.ShortDescription,
                Images = entity.Images.Select(x => new ImageModel()
                {
                    Name = x.Name,
                    Url = x.Url
                }).ToList(),
                Params = entity.Params.Select( x => new DetailParamModel()
                {
                    Key = x.Group.Name,
                    IsFilter = x.Group.IsFilter,
                    Value = x.Value
                }).ToList(),
                SuitableCars = entity.Cars.Select( x => new CarMapper().Map(x)).ToList()
            };
            //-- set main image
            detail.MainImage = detail.Images.FirstOrDefault() ?? new ImageModel()
            {
                Name = detail.Name,
                Url = "/img/empty-image.png"
            };

            return detail;
        }
    }
}