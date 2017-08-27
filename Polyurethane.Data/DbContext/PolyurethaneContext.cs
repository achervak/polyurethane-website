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
        public DbSet<ParameterEntity> DetailParams { get; set; }
        public DbSet<ParamGroupEntity> ParamGroup { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<UserEntity> Customers { get; set; }
        public DbSet<SmtpConfiguration> SmtpConfigurations { get; set; }
        public DbSet<ConfigurationEntiy> Configurations { get; set; }
        public DbSet<TemplatesEntity> Templates { get; set; }

        //public DbSet<DetailInCarEntity> DetailsInCar { get; set; }
        public DbSet<ImageInCarEntity> ImagesInCar { get; set; }
        //public DbSet<ImageInDetailEntity> ImagesInDetail { get; set; }

        public PolyurethaneContext(string connectionString) : base(connectionString)
        {
            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            Database.SetInitializer<PolyurethaneContext>(new PolyurethaneContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailEntity>()
                .HasMany(x => x.Images)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("DetailId");
                    x.MapRightKey("ImageId");
                    x.ToTable("ImageInDetail");
                });

            modelBuilder.Entity<CarEntity>()
                .HasMany(x => x.Details)
                .WithMany(x => x.Cars)
                .Map(x =>
                {
                    x.MapLeftKey("CarId");
                    x.MapRightKey("DetailId");
                    x.ToTable("DetailInCar");
                });
            
            modelBuilder.Entity<DetailEntity>()
                .HasMany(x => x.Params)
                .WithRequired( x => x.Detail )
                .HasForeignKey(x => x.DetailId);

            //modelBuilder.Entity<ParameterEntity>()
            //    .HasRequired(x => x.Group)
            //    .WithMany()
            //    .HasForeignKey(x => x.Group);

            modelBuilder.Entity<ParameterEntity>()
                .HasRequired(x => x.Group)
                .WithMany(x => x.Parameters)
                .Map(x => x.MapKey("GroupId"));

            modelBuilder.Entity<OrderEntity>()
                .HasRequired(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
            


                //x.
                //.HasRequired(x => x.Group)
                //.WithMany()
                //.HasForeignKey(x => x.Group);


            //// Relationship one to many
            //modelBuilder.Entity<DetailEntity>()
            //    .HasMany(x => x.ImagesLinks)
            //    .WithOptional()
            //    .HasForeignKey(x => x.DetailId);

            //// Relationship one to many
            //modelBuilder.Entity<ImageInDetailEntity>()
            //    .HasRequired(x => x.Image)
            //    .WithMany()
            //    .HasForeignKey(x => x.ImageId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
