﻿// <auto-generated />
using System;
using HMT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMT.Migrations
{
    [DbContext(typeof(HMTContext))]
    [Migration("20230416103507_addIdentity")]
    partial class addIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMT.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameCategory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HMT.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NameProduct")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HMT.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Request_Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int?>("TotalQuantity")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserManagerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserManagerId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("HMT.Models.RequestDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestDetails");
                });

            modelBuilder.Entity("HMT.Models.TotalProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("TotalProducts");
                });

            modelBuilder.Entity("HMT.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            Address = "Hà Nội",
                            ConcurrencyStamp = "df1daec9-3390-4a24-a338-78fa632cdd20",
                            Email = "miranda@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Miranda",
                            Image = "julian-wan-WNoLnJo7tS8-unsplash.jpg",
                            LockoutEnabled = false,
                            NormalizedEmail = "MIRANDA@GMAIL.COM",
                            NormalizedUserName = "miranda",
                            PasswordHash = "AQAAAAIAAYagAAAAEHwqVf134pF6GYa4lEel/HgcBVtayDUbWt2mZxPL2TQWCOpVO6twH11XX9BZW9gEYQ==",
                            PhoneNumber = "0366666999",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a830f47c-6174-4e28-adb7-6daf939d1705",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            Address = "Hà Nội",
                            ConcurrencyStamp = "6d83dea8-6e01-497c-9785-b1bc6c954bcd",
                            Email = "lukeivory@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Luke Ivory",
                            Image = "pretty-smiling-joyfully-female-with-fair-hair-dressed-casually-looking-with-satisfaction.jpg",
                            LockoutEnabled = false,
                            NormalizedEmail = "LUKEIVORY@GMAIL.COM",
                            NormalizedUserName = "lukeivory",
                            PasswordHash = "AQAAAAIAAYagAAAAEFdadZ7GZRXb90aNC/ENAA7ErlUlsos3Av3rOi/PZYfpPArqUc8eMmZ5IYudpH7CrQ==",
                            PhoneNumber = "0366634999",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bc554a2e-853c-4319-8744-0621427740e7",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "3",
                            AccessFailedCount = 0,
                            Address = "Hà Nội",
                            ConcurrencyStamp = "fc31312d-fe19-49bc-b1e0-e0c7138beeb0",
                            Email = "andyking11@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Andy King",
                            Image = "nicolas-horn-MTZTGvDsHFY-unsplash.jpg",
                            LockoutEnabled = false,
                            NormalizedEmail = "ANDYKING11@GMAIL.COM",
                            NormalizedUserName = "andyking",
                            PasswordHash = "AQAAAAIAAYagAAAAEJskerV+4GUWratBAkZ0FKBkCpa1157FE4Lplp91uPNyVVzZ4s8+u9vyNPxVAkANWQ==",
                            PhoneNumber = "0399999666",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7658a695-311f-4f5b-9bca-d85df35108c0",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "4",
                            AccessFailedCount = 0,
                            Address = "Hà Nội",
                            ConcurrencyStamp = "7170f69d-384c-4721-af5c-5244c00556f0",
                            Email = "irenecollins@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Irene Collins",
                            Image = "portrait-young-asia-lady-with-positive-expression-arms-crossed-smile-broadly-dressed-casual-clothing-looking-camera-pink-background.jpg",
                            LockoutEnabled = false,
                            NormalizedEmail = "IRENECOLLINS@GMAIL.COM",
                            NormalizedUserName = "irenecollins",
                            PasswordHash = "AQAAAAIAAYagAAAAEPeILHOHctNhw0n5Jr3sVMYO36+2XUWc+NLt42CangvFScBa/yb8upC4/KtO8/GpXA==",
                            PhoneNumber = "0366666956",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e319926f-d428-429e-979f-bb54978d03e3",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "5",
                            AccessFailedCount = 0,
                            Address = "Hà Nội",
                            ConcurrencyStamp = "b91a4bda-e99b-4a16-8d37-144dbc529247",
                            Email = "lauriefox@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Laurie Fox",
                            Image = "lifestyle-beauty-fashion-people-emotions-concept-young-asian-female-office-manager-ceo-with-pleased-expression-standing-white-background-smiling-with-arms-crossed-chest.jpg",
                            LockoutEnabled = false,
                            NormalizedEmail = "LAURIEFOX@GMAIL.COM",
                            NormalizedUserName = "lauriefox",
                            PasswordHash = "AQAAAAIAAYagAAAAENdVo0dVOqiL8rM9hGHxWYNlTfpzolox84REnJzuK9UAeeETGAl1OGW+u6+NWPTXvA==",
                            PhoneNumber = "0333666666",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c7ce01e8-6cab-482a-8a22-f25b7047c049",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Director",
                            NormalizedName = "DIRECTOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "3"
                        },
                        new
                        {
                            UserId = "2",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "3",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "4",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "5",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("HMT.Models.Product", b =>
                {
                    b.HasOne("HMT.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HMT.Models.Request", b =>
                {
                    b.HasOne("HMT.Models.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMT.Models.User", "UserManager")
                        .WithMany("RequestsFromManager")
                        .HasForeignKey("UserManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserManager");
                });

            modelBuilder.Entity("HMT.Models.RequestDetail", b =>
                {
                    b.HasOne("HMT.Models.Product", "Product")
                        .WithMany("RequestDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMT.Models.Request", "Request")
                        .WithMany("RequestDetails")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("HMT.Models.TotalProduct", b =>
                {
                    b.HasOne("HMT.Models.Product", "Product")
                        .WithMany("TotalProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HMT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HMT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HMT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HMT.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HMT.Models.Product", b =>
                {
                    b.Navigation("RequestDetails");

                    b.Navigation("TotalProducts");
                });

            modelBuilder.Entity("HMT.Models.Request", b =>
                {
                    b.Navigation("RequestDetails");
                });

            modelBuilder.Entity("HMT.Models.User", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("RequestsFromManager");
                });
#pragma warning restore 612, 618
        }
    }
}
