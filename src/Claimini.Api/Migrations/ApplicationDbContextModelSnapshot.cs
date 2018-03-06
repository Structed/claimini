﻿// <auto-generated />
using Claimini.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Claimini.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Claimini.Api.Data.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedTimestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Claimini.Api.Data.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("State")
                        .HasMaxLength(20);

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("StreetAddressAdditional")
                        .HasMaxLength(50);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Claimini.Api.Data.Invoice", b =>
                {
                    b.Property<int>("IdSql")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedTimestamp");

                    b.Property<int>("CustomerId");

                    b.Property<long>("PaidTimestamp");

                    b.HasKey("IdSql");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Claimini.Api.Data.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleId");

                    b.Property<int?>("InvoiceIdSql");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("VatPercentage");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("InvoiceIdSql");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("Claimini.Api.Data.Invoice", b =>
                {
                    b.HasOne("Claimini.Api.Data.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Claimini.Api.Data.InvoiceItem", b =>
                {
                    b.HasOne("Claimini.Api.Data.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Claimini.Api.Data.Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceIdSql");
                });
#pragma warning restore 612, 618
        }
    }
}
