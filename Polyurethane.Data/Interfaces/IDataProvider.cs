using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models;

namespace Polyurethane.Data.Interfaces
{
    public interface IDataProvider
    {
        Task<CarEntity> CreateCar(CarEntity car);
        Task<DetailEntity> CreateDetail(DetailEntity detail);
        Task<DetailEntity> UpdateDetail(DetailEntity detail);
        Task<DetailEntity> SetDetailParams(DetailEntity detail, IEnumerable<DetailParamModel> detailParams);
        Task<ImageEntity> CreateImage(ImageEntity image);
        Task<ImageEntity> AddImageToDetail(DetailEntity detail, ImageEntity image);
        Task<IEnumerable<DetailEntity>> GetDetails(Func<DetailEntity> filter, int count);
        Task<DetailEntity> GetDetail(Guid guid);
        
    }
}
