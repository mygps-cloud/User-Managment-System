using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.DbContextSql
{
    public class IpCheck :Microsoft.EntityFrameworkCore. DbContext
    {
        public IpCheck(DbContextOptions<IpCheck> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<IpStatus> IpStatuses { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.IpStatuses)
                .WithOne() 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Device>()
                .HasOne(i => i.IpStatus)
                .WithOne()
                .HasForeignKey<Device>(d => d.IpStatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
