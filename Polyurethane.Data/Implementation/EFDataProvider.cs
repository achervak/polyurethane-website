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


        public string ConnectionString {
            get { return _connectionString; }
        }

        public async Task<IEnumerable<EventEntity>> GetEvents(Expression<Func<EventEntity, bool>> filter = null)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                if (filter == null)
                    return await db.Events.OrderByDescending(x => x.EventTime).Take(1000).ToListAsync()
                        .ConfigureAwait(continueOnCapturedContext: false);

                return await db.Events
                    .Where(filter)
                    .OrderByDescending(x => x.EventTime)
                    .Take(1000)
                    .ToListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
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
                return await db.Details.Include(x => x.Images).Include("Params.Group").Include(x => x.Cars).FirstOrDefaultAsync(x => x.Id == guid);
            }
        }

        public async Task<IEnumerable<CarEntity>> GetCars(Expression<Func<CarEntity, bool>> filter)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                return await db.Cars.Include(x => x.Details)
                    .Where(filter)
                    .ToListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task<IEnumerable<CarEntity>> GetCars()
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                return await db.Cars.Include(x => x.Details)
                    .ToListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
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

        public async Task<DetailEntity> SetDetailCars(DetailEntity detail, IEnumerable<string> guids)
        {
            using (var db = new PolyurethaneContext(_connectionString))
            {
                db.Details.Attach(detail);
                var dbCars = db.Cars.Where(x => guids.Contains(x.Id.ToString()));
                //-- remove unsuitable cars

                //-- add new cars
                detail.Cars.Clear();
                foreach (var dbCar in dbCars)
                {
                    detail.Cars.Add(dbCar);
                }

               await db.SaveChangesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

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
                return await db.Details
                    .Include(x => x.Images)
                    .Include(x => x.Cars)
                    .Include("Params.Group")
                    .Where(filter)
                    .ToListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }
    }
}
