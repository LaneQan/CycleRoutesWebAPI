﻿// <auto-generated />
using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CycleRoutesCore.Domain.Migrations
{
    [DbContext(typeof(CycleRoutesContext))]
    partial class CycleRoutesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RouteId");

                    b.Property<bool>("State");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Landscape");

                    b.Property<double>("Length");

                    b.Property<int>("LineType");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.RouteImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteImages");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Image");

                    b.Property<bool>("IsAdministrator");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.Like", b =>
                {
                    b.HasOne("CycleRoutesCore.Domain.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.HasOne("CycleRoutesCore.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.Route", b =>
                {
                    b.HasOne("CycleRoutesCore.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CycleRoutesCore.Domain.Models.RouteImage", b =>
                {
                    b.HasOne("CycleRoutesCore.Domain.Models.Route")
                        .WithMany("Images")
                        .HasForeignKey("RouteId");
                });
#pragma warning restore 612, 618
        }
    }
}
