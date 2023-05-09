using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HMT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HMT.Data
{
    public class HMTContext : IdentityDbContext<User>
    {
        public HMTContext(DbContextOptions<HMTContext> options)
            : base(options)
        {
        }

        public DbSet<HMT.Models.Product> Products { get; set; } = default!;
        public DbSet<HMT.Models.Category> Categories { get; set; } = default!;
        public DbSet<HMT.Models.Request> Requests { get; set; } = default!;
        public DbSet<HMT.Models.RequestDetail> RequestDetails { get; set; } = default!;
        public DbSet<HMT.Models.TotalProduct> TotalProducts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }


        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Employee", NormalizedName = "EMPLOYEE" },
                new IdentityRole() { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "3", Name = "Director", NormalizedName = "DIRCETOR" }
                );
        }
        private void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    FullName = "Miranda",
                    UserName = "Miranda",
                    NormalizedUserName = "miranda",
                    Email = "miranda@gmail.com",
                    NormalizedEmail = "MIRANDA@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumber = "0366666999",
                    Address = "Hà Nội",
                    Image = "julian-wan-WNoLnJo7tS8-unsplash.jpg",
                    LockoutEnabled = false,
                },

                new User
                {
                    Id = "2",
                    FullName = "Luke Ivory",
                    UserName = "LukeIvory",
                    NormalizedUserName = "lukeivory",
                    Email = "lukeivory@gmail.com",
                    NormalizedEmail = "LUKEIVORY@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumber = "0366634999",
                    Address = "Hà Nội",
                    Image = "pretty-smiling-joyfully-female-with-fair-hair-dressed-casually-looking-with-satisfaction.jpg",
                    LockoutEnabled = false,
                },

                new User
                {
                    Id = "3",
                    FullName = "Andy King",
                    UserName = "AndyKing",
                    NormalizedUserName = "andyking",
                    Email = "andyking11@gmail.com",
                    NormalizedEmail = "ANDYKING11@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumber = "0399999666",
                    Address = "Hà Nội",
                    Image = "nicolas-horn-MTZTGvDsHFY-unsplash.jpg",
                    LockoutEnabled = false,
                },

                new User
                {
                    Id = "4",
                    FullName = "Irene Collins",
                    UserName = "IreneCollins",
                    NormalizedUserName = "irenecollins",
                    Email = "irenecollins@gmail.com",
                    NormalizedEmail = "IRENECOLLINS@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumber = "0366666956",
                    Address = "Hà Nội",
                    Image = "portrait-young-asia-lady-with-positive-expression-arms-crossed-smile-broadly-dressed-casual-clothing-looking-camera-pink-background.jpg",
                    LockoutEnabled = false,
                },

                new User
                {
                    Id = "5",
                    FullName = "Laurie Fox",
                    UserName = "LaurieFox",
                    NormalizedUserName = "lauriefox",
                    Email = "lauriefox@gmail.com",
                    NormalizedEmail = "LAURIEFOX@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumber = "0333666666",
                    Address = "Hà Nội",
                    Image = "lifestyle-beauty-fashion-people-emotions-concept-young-asian-female-office-manager-ceo-with-pleased-expression-standing-white-background-smiling-with-arms-crossed-chest.jpg",
                    LockoutEnabled = false,
                }

            );
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "3", UserId = "1" },
                new IdentityUserRole<string>() { RoleId = "2", UserId = "2" },
                new IdentityUserRole<string>() { RoleId = "2", UserId = "3" },
                new IdentityUserRole<string>() { RoleId = "1", UserId = "4" },
                new IdentityUserRole<string>() { RoleId = "1", UserId = "5" }
                );
        }
    }

}
