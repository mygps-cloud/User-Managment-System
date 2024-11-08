using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql
{
    public class DbIpCheck : DbContext
    {
        public DbIpCheck(DbContextOptions<DbIpCheck> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<IpStatus> IpStatuses { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PingLog> PingLog { get; set; }
        public DbSet<WorkSchedule> workSchedules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
            
        }

    }

    //   public class Factory : IDesignTimeDbContextFactory<DbIpCheck>
    // {
    //     public DbIpCheck CreateDbContext(string[] args)
    //     {
    //         var config = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //            .Build();

    //         var optionsBuilder = new DbContextOptionsBuilder<DbIpCheck>();
    //         var connectionString = config.GetConnectionString("DB");
    //           optionsBuilder.UseMySql(connectionString, 
    //           new MySqlServerVersion(new Version(8, 0, 21)));
    
    //         return new DbIpCheck(optionsBuilder.Options);
    //     }
    // }
}