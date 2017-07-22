using System;
using System.Threading.Tasks;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;

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
    }
}
