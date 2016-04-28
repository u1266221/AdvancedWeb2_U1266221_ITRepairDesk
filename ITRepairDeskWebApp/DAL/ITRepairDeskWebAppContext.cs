using ITRepairDeskWebApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ITRepairDeskWebApp.DAL
{
    public class ITRepairDeskWebAppContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<ClientOffice> ClientOffices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Job>()
                .HasMany(c => c.Clients).WithMany(i => i.Jobs)
                .Map(t => t.MapLeftKey("JobID")
                    .MapRightKey("ClientID")
                    .ToTable("ClientJob"));
        }
    }
}