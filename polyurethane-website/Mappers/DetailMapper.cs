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
                Name = entity.Name,
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
                }).ToList()
            };

            return detail;
        }
    }
}