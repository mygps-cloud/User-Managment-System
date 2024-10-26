﻿// <auto-generated />
using System;
using Ipstatuschecker.DbContextSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ipstatuschecker.Migrations
{
    [DbContext(typeof(DbIpCheck))]
    partial class DbIpCheckModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.Device", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceNames")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IpStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IpStatusId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.IpStatus", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IpAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IpStatuses");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.PingLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("OflineTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OnlieTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PingLog");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.Device", b =>
                {
                    b.HasOne("Ipstatuschecker.DomainEntity.IpStatus", "IpStatus")
                        .WithOne()
                        .HasForeignKey("Ipstatuschecker.DomainEntity.Device", "IpStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ipstatuschecker.DomainEntity.User", null)
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IpStatus");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.IpStatus", b =>
                {
                    b.HasOne("Ipstatuschecker.DomainEntity.User", null)
                        .WithMany("IpStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.PingLog", b =>
                {
                    b.HasOne("Ipstatuschecker.DomainEntity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ipstatuschecker.DomainEntity.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("IpStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
