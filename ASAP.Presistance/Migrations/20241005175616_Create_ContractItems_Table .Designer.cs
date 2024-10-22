﻿// <auto-generated />
using System;
using ASAP.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241005175616_Create_ContractItems_Table ")]
    partial class Create_ContractItems_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ASAP.Domain.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<int>("ContractTypeId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ContractorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PostCode")
                        .HasColumnType("longtext");

                    b.Property<string>("SiteAgent")
                        .HasColumnType("longtext");

                    b.Property<string>("TLO_Mobile")
                        .HasColumnType("longtext");

                    b.Property<string>("TLO_Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.ContractItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<int?>("Ancils")
                        .HasColumnType("int");

                    b.Property<int?>("Ancils_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("Ancils_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Bifolds")
                        .HasColumnType("int");

                    b.Property<int?>("Bifolds_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("Bifolds_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("CertesNo")
                        .HasColumnType("int");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("FD")
                        .HasColumnType("int");

                    b.Property<int?>("FED")
                        .HasColumnType("int");

                    b.Property<int?>("FED_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("FED_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FitterId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Frame")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("GlassDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("GlassStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InstallationDateFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("InstallationDateTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("InvoiceNo")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext");

                    b.Property<int?>("PD")
                        .HasColumnType("int");

                    b.Property<int?>("PD_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("PD_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("PanelDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PanelStatus")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<int?>("ProductionWeek")
                        .HasColumnType("int");

                    b.Property<int?>("RD")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Requirement")
                        .HasColumnType("int");

                    b.Property<int?>("Roofs")
                        .HasColumnType("int");

                    b.Property<int?>("Roofs_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("Roofs_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("SurveyorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<int?>("VS")
                        .HasColumnType("int");

                    b.Property<int?>("VS_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("VS_SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("W")
                        .HasColumnType("int");

                    b.Property<int?>("W_RD_FD_Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("W_RD_FD_SupplierId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("contractItems");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.Contractor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Sorted")
                        .HasColumnType("int");

                    b.Property<int>("SupplierTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.Contract", b =>
                {
                    b.HasOne("ASAP.Domain.Entities.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId");

                    b.Navigation("Contractor");
                });

            modelBuilder.Entity("ASAP.Domain.Entities.ContractItem", b =>
                {
                    b.HasOne("ASAP.Domain.Entities.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });
#pragma warning restore 612, 618
        }
    }
}
