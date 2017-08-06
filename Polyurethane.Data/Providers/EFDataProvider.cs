using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Models;

namespace Polyurethane.Data.Providers
{
    public class EfDataProvider : IDataProvider
    {
        private readonly string _connectionString;

        public EfDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public async Task<CarEntity> CreateCar(CarEntity car)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Cars.Add(car);
                await db.SaveChangesAsync();
                return car;
            }
        }

        public async Task<DetailEntity> CreateDetail(DetailEntity detail)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Details.Add(detail);
                await db.SaveChangesAsync();
                return detail;
            }
        }

        public async Task<DetailEntity> GetDetail(Guid guid)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Configuration.LazyLoadingEnabled = false;
                return await db.Details.Include(x => x.Images).Include("Params.Group").FirstOrDefaultAsync(x => x.Id == guid);
            }
        }

        public async Task<List<ParamGroupEntity>> GetParamGroups(string filter)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                if(string.IsNullOrEmpty(filter))
                    return await db.ParamGroup.ToListAsync();

                return await db.ParamGroup.Where(x => x.Name.Contains(filter)).ToListAsync();
            }
        }

        public async Task<DetailEntity> UpdateDetail(DetailEntity detail)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Details.Attach(detail);
                await db.SaveChangesAsync();
                return detail;
            }
        }

        public async Task<DetailEntity> SetDetailParams(DetailEntity detail, IEnumerable<DetailParamModel> detailParams)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                foreach (var param in detailParams)
                {
                    var dbGroup = await db.ParamGroup.FirstOrDefaultAsync(x => x.Name == param.Key);
                    if (dbGroup == null)
                    {
                        dbGroup = new ParamGroupEntity()
                        {
                            Name = param.Key
                        };
                        db.ParamGroup.Add(dbGroup);
                        await db.SaveChangesAsync();
                    }

                    dbGroup.IsFilter = param.IsFilter;

                    var detailParam = detail.Params.FirstOrDefault(x => x.Group.Name == param.Key);
                    if (detailParam == null)
                    {
                        detailParam = new ParameterEntity()
                        {
                            DetailId = detail.Id,
                            Group = dbGroup,
                        };
                        db.DetailParams.Add(detailParam);
                        await db.SaveChangesAsync();
                        detail.Params.Add(detailParam);
                    }
                    detailParam.Value = param.Value;

                    await db.SaveChangesAsync();
                }

                return detail;
            }
        }

        public async Task<ImageEntity> CreateImage(ImageEntity image)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Images.Add(image);
                await db.SaveChangesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                return image;
            }
        }

        public async Task<ImageEntity> AddImageToDetail(DetailEntity detail, ImageEntity image)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                var dbDetail = await db.Details
                    .FirstOrDefaultAsync(x => x.Id == detail.Id)
                    .ConfigureAwait(continueOnCapturedContext: false);

                if (dbDetail == null)
                    return null;

                dbDetail.Images.Add(image);
                await db.SaveChangesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                return image;
            }
        }

        public async Task<int> GetDetailsCount(Expression<Func<DetailEntity, bool>> filter)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                return await db.Details./*Where(filter).*/CountAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task<IEnumerable<DetailEntity>> GetDetails(Expression<Func<DetailEntity, bool>> filter, int pageSize, int pageNumber)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                return await db.Details.Include(x => x.Images)
                    .Include("Params.Group")
                    .Where(filter)
                    .ToListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }
    }
}
