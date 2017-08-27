using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models;

namespace Polyurethane.Data.Interfaces
{
    public interface IDataProvider
    {
        string ConnectionString { get; }
        Task<CarEntity> CreateCar(CarEntity car);
        Task<DetailEntity> CreateDetail(DetailEntity detail);
        Task<DetailEntity> UpdateDetail(DetailEntity detail);
        Task<DetailEntity> SetDetailParams(DetailEntity detail, IEnumerable<DetailParamModel> detailParams);
        Task<DetailEntity> SetDetailCars(DetailEntity detail, IEnumerable<string> carsGuid);
        Task<ImageEntity> CreateImage(ImageEntity image);
        Task<ImageEntity> AddImageToDetail(DetailEntity detail, ImageEntity image);
        Task<int> GetDetailsCount(Expression<Func<DetailEntity, bool>> filter);
        Task<IEnumerable<DetailEntity>> GetDetails(Expression<Func<DetailEntity, bool>> filter, int pageSize, int pageNumber);
        Task<DetailEntity> GetDetail(Guid guid);
        Task<IEnumerable<CarEntity>> GetCars(Expression<Func<CarEntity, bool>> filter);
        Task<IEnumerable<CarEntity>> GetCars();
        Task<IEnumerable<EventEntity>> GetEvents(Expression<Func<EventEntity, bool>> filter = null);


        Task<List<ParamGroupEntity>> GetParamGroups(string filter);
        //Task<DetailEntity> GetDetails(Expression<Func<DetailEntity, bool>> filter);


    }
}
