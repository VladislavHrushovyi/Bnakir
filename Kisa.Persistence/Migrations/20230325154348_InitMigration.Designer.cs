﻿// <auto-generated />
using System;
using Kisa.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kisa.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230325154348_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kisa.Domain.Entities.KisaCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ExpireTo")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("KisaCards");
                });

            modelBuilder.Entity("Kisa.Domain.Entities.KisaMain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<float>("CommissionBetweenCardSystem")
                        .HasColumnType("real");

                    b.Property<float>("CommissionBetweenCountry")
                        .HasColumnType("real");

                    b.Property<float>("CommissionInCountry")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("KisaMain");
                });
#pragma warning restore 612, 618
        }
    }
}
