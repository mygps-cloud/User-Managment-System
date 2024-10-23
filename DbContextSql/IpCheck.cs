
using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;
using IpStatus = Ipstatuschecker.DomainEntity.IpStatus;

namespace Ipstatuschecker.DbContextSql
{
    public class IpCheck : Microsoft.EntityFrameworkCore.DbContext
{ public IpCheck(DbContextOptions<IpCheck> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<IpStatus> IpStatuses { get; set; }
    public DbSet<Device> Devices { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasMany(u => u.IpStatuses)
        .WithOne(i => i._User)
        .HasForeignKey(i => i.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<User>()
        .HasMany(u => u.Devices)
        .WithOne(d => d.User)
        .HasForeignKey(d => d.UserId)
        .OnDelete(DeleteBehavior.Cascade);

   modelBuilder.Entity<IpStatus>()
    .HasOne(u => u._Device) 
    .WithOne(d => d.IpStatus) 
    .HasForeignKey<Device>(d => d.IpStatusId) 
    .OnDelete(DeleteBehavior.Cascade);

}

}

    
}