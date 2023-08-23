﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyApplication.Persistence;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    [DbContext(typeof(SurveyApplicationDbContext))]
    partial class SurveyApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SurveyApplication.Domain.BangKhaoSat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("MaBangKhaoSat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaDotKhaoSat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaLoaiHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenBangKhaoSat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BangKhaoSats");
                });

            modelBuilder.Entity("SurveyApplication.Domain.CauHoi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<bool?>("BatBuoc")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("MaCauHoi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaLoaiCauHoi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("SurveyApplication.Domain.DonVi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MaDonVi")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaLinhVuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaLoaiHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaSoThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDonVi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DonVis");
                });

            modelBuilder.Entity("SurveyApplication.Domain.DotKhaoSat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("MaDotKhaoSat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaLoaiHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDotKhaoSat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DotKhaoSats");
                });

            modelBuilder.Entity("SurveyApplication.Domain.GuiEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("DiaChiNhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MaGuiEmail")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TieuDe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GuiEmails");
                });

            modelBuilder.Entity("SurveyApplication.Domain.LinhVucHoatDong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<Guid>("MaLinhVuc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("TenLinhVuc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LinhVucHoatDongs");
                });

            modelBuilder.Entity("SurveyApplication.Domain.LoaiCauHoi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<Guid>("MaLoaicauHoi")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("TenLoaiCauHoi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoaiCauHoiVis");
                });

            modelBuilder.Entity("SurveyApplication.Domain.LoaiHinhDonVi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("MaLoaiHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("TenLoaiHinh")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoaiHinhDonVis");
                });

            modelBuilder.Entity("SurveyApplication.Domain.NguoiDaiDien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<string>("ChucVu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MaDonVi")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaNguoiDaiDien")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NguoiDaiDiens");
                });

            modelBuilder.Entity("SurveyApplication.Domain.NguoiDung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NguoiDungs");
                });

            modelBuilder.Entity("SurveyApplication.Domain.NguoiDungVaiTro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<int>("MaNguoiDungVaiTro")
                        .HasColumnType("int");

                    b.Property<int>("MaVaiTro")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NguoiDungVaiTros");
                });

            modelBuilder.Entity("SurveyApplication.Domain.Quyen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("MaQuyen")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("TenQuyen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Quyens");
                });

            modelBuilder.Entity("SurveyApplication.Domain.VaiTro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("MaVaiTro")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<int?>("TenVaiTro")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("VaiTros");
                });

            modelBuilder.Entity("SurveyApplication.Domain.VaiTroQuyen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActiveFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("MaQuyen")
                        .HasColumnType("int");

                    b.Property<int>("MaVaiTro")
                        .HasColumnType("int");

                    b.Property<int>("MaVaiTroQuyen")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("VaiTroQuyens");
                });
#pragma warning restore 612, 618
        }
    }
}
