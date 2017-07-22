using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Initializers;

namespace Polyurethane.Data.DbContext
{
    public class PolyurethaneContext : System.Data.Entity.DbContext
    {
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<DetailEntity> Details { get; set; }
        public DbSet<DetailInCarEntity> DetailsInCar { get; set; }
        public DbSet<ImageInCarEntity> ImagesInCar { get; set; }
        public DbSet<ImageInDetailEntity> ImagesInDetail { get; set; }

        public PolyurethaneContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<PolyurethaneContext>(new PolyurethaneContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
