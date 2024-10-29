using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.DbContextSql
{
    public class DbIpCheck :Microsoft.EntityFrameworkCore. DbContext
    {
        public DbIpCheck(DbContextOptions<DbIpCheck> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<IpStatus> IpStatuses { get; set; }
        public DbSet<Device> Devices { get; set; } 
        public DbSet<PingLog> PingLog { get; set; }
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

            modelBuilder.Entity<PingLog>()
                .HasOne(p => p.User) 
                .WithOne(u => u.PingLog) 
                .HasForeignKey<PingLog>(p => p.UserId) 
                .OnDelete(DeleteBehavior.SetNull); 



                //   modelBuilder.Entity<PingLog>()
                // .HasOne(p => p.User) 
                // .WithMany(u => u.PingLog) 
                // .HasForeignKey(param=>param.UserId)
                // .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
