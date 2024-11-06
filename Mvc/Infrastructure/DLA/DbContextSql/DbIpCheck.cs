using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql
{
    public class DbIpCheck : DbContext
    {
        public DbIpCheck(DbContextOptions<DbIpCheck> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<IpStatus> IpStatuses { get; set; }
        public DbSet<Device> Devices { get; set; } 
        public DbSet<PingLog> PingLog { get; set; }
         public DbSet<WorkSchedule> workSchedules   { get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.IpStatuses)
                .WithOne() 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Device>()
                .HasOne(i => i.IpStatus)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

         modelBuilder.Entity<User>()
                .HasOne(u => u.workSchedule)
                .WithOne()
                .HasForeignKey<WorkSchedule>(ws => ws.UserId) 
                .OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<PingLog>()
                .HasOne(p => p.User) 
                .WithOne(u => u.PingLog) 
                .HasForeignKey<PingLog>(p => p.UserId) 
                .OnDelete(DeleteBehavior.SetNull); 


        }
    }
}
