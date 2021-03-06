﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.API.Database;

namespace Orders.API.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20200226164438_CreateOrderDB")]
    partial class CreateOrderDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Orders.API.Models.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DocumentNumber");

                    b.Property<string>("DocumentType");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Orders.API.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<long?>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<float>("Quantity");

                    b.Property<string>("Sku");

                    b.Property<bool>("Taxable");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Orders.API.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppId");

                    b.Property<string>("Channel");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Currency");

                    b.Property<long>("CustomerId");

                    b.Property<string>("ShippingId");

                    b.Property<string>("Status");

                    b.Property<decimal>("Total");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Orders.API.Models.Item", b =>
                {
                    b.HasOne("Orders.API.Models.Order")
                        .WithMany("ItemList")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
