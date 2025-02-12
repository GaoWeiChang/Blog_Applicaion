using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "b038555c-bf02-4685-94e9-7fc8750933d8";
            var writerRoleId = "c2088cde-4778-42bb-81fa-64e1aad6ebe2";

            // Create Reader and Writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            // Seeding roles หมายถึงการเพิ่มข้อมูลเริ่มต้น (initial data) ของ roles เข้าไปในฐานข้อมูลโดยอัตโนมัติตอนที่ database ถูกสร้างขึ้น
            builder.Entity<IdentityRole>().HasData(roles); // ใช้ HasData() เพื่อบอก Entity Framework ให้เพิ่มข้อมูลเหล่านี้ลงในตารางที่เกี่ยวข้องเมื่อรัน migration

            // Create Admin user
            var adminUserId = "f5ce0df5-b999-4405-9e9c-f7d57aab78be";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123"); // สร้างรหัสผ่านสำหรับ Admin
            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles To Admin (admin has right to be reader and writer)
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

            /*
                ตาราง AspNetUsers (IdentityUser) - store user data
                ตาราง AspNetRoles (IdentityRole) - store roles
                ตาราง AspNetUserRoles (IdentityUserRole) - store the relationship between roles and users
            */
        }
    }
}
