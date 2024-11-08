

using Ipstatuschecker.DomainEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipstatuschecker.Mvc.Infrastructure.DLA.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {  
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(30).IsRequired(false);
            
            builder
                .HasMany(u => u.IpStatuses)
                .WithOne()
                .HasForeignKey(ip => ip.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(u => u.Devices)
                .WithOne()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);
                 

            builder.HasOne(u => u.PingLog)
                .WithOne(pl => pl.User)
                .HasForeignKey<PingLog>(pl => pl.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.workSchedule)
                .WithOne(ws => ws.User)
                .HasForeignKey<WorkSchedule>(ws => ws.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
