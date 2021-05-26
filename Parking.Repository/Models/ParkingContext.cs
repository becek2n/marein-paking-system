using Parking.Repository.Seed;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Models
{
    public partial class ParkingContext : DbContext
    {
        public ParkingContext() : base("ParkingContext")
        {
            Database.SetInitializer<ParkingContext>(new ParkingInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Transportation> Transportations { get; set; }
        public virtual DbSet<ParkingArea> Parkings { get; set; }

    }
}
