using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BloodBankModels
{
    public class BloodBankDbContext:DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<BloodGroup> Bloods { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<ReportSearch> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
