using ITRepairDeskWebApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ITRepairDeskWebApp.DAL
{
    public class ITRepairDeskWebAppContext : DbContext
    {

        public ITRepairDeskWebAppContext() : base("ITRepairDeskWebAppContext")
        {
        }

        public DbSet<Technician> Technicians { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}