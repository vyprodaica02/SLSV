﻿// <auto-generated />
using System;
using Infrastructure.DataEx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230927041532_initial1")]
    partial class initial1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Design.Entity.KetQua", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("DiemThi")
                        .HasColumnType("float");

                    b.Property<int>("LanThi")
                        .HasColumnType("int");

                    b.Property<int>("MaMonHoc")
                        .HasColumnType("int");

                    b.Property<int>("MaSinhVien")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaMonHoc");

                    b.HasIndex("MaSinhVien");

                    b.ToTable("ketQuas");
                });

            modelBuilder.Entity("Design.Entity.Khoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenKhoa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("khoas");
                });

            modelBuilder.Entity("Design.Entity.KhoaMon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaKhoa")
                        .HasColumnType("int");

                    b.Property<int>("MaMH")
                        .HasColumnType("int");

                    b.Property<string>("TinChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TongSoTiet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaKhoa");

                    b.ToTable("khoaMons");
                });

            modelBuilder.Entity("Design.Entity.Lop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaKhoa")
                        .HasColumnType("int");

                    b.Property<int>("SiSo")
                        .HasColumnType("int");

                    b.Property<string>("TenLop")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaKhoa");

                    b.ToTable("lops");
                });

            modelBuilder.Entity("Design.Entity.MonHoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("monHocs");
                });

            modelBuilder.Entity("Design.Entity.SinhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoSinhVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HocBong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaLop")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiSinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenSinhVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avata")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("roll")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaLop");

                    b.ToTable("sinhViens");
                });

            modelBuilder.Entity("Design.Entity.KetQua", b =>
                {
                    b.HasOne("Design.Entity.MonHoc", "MonHoc")
                        .WithMany()
                        .HasForeignKey("MaMonHoc")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Design.Entity.SinhVien", "SinhVien")
                        .WithMany()
                        .HasForeignKey("MaSinhVien")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MonHoc");

                    b.Navigation("SinhVien");
                });

            modelBuilder.Entity("Design.Entity.KhoaMon", b =>
                {
                    b.HasOne("Design.Entity.Khoa", "Khoa")
                        .WithMany()
                        .HasForeignKey("MaKhoa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("Design.Entity.Lop", b =>
                {
                    b.HasOne("Design.Entity.Khoa", "khoa")
                        .WithMany()
                        .HasForeignKey("MaKhoa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("khoa");
                });

            modelBuilder.Entity("Design.Entity.SinhVien", b =>
                {
                    b.HasOne("Design.Entity.Lop", "Lop")
                        .WithMany()
                        .HasForeignKey("MaLop")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Lop");
                });
#pragma warning restore 612, 618
        }
    }
}
