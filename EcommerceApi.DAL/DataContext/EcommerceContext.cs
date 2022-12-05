using EcommerceApi.DAL.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EcommerceApi.DAL.DataContext
{
    public class EcommerceContext : IdentityDbContext<User>
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPayment> UserPayment { get; set; }

        //Default Users
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Creating Roles
            string customerRoleId = Guid.NewGuid().ToString();
            string adminRoleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "customer",
                    NormalizedName = "CUSTOMER",
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId
                },
                new IdentityRole()
                {
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                }
            );

            //User Id
            string johnId = Guid.NewGuid().ToString();
            string johnEmail = "john@email.com";
            string deanId = Guid.NewGuid().ToString();
            string deanEmail = "dean@email.com";
            string adminId = Guid.NewGuid().ToString();
            string adminEmail = "admin@ecom.com";


            //Creating Users
            var john = new User
            {
                Id = johnId,
                Email = johnEmail,
                NormalizedEmail = johnEmail.ToUpper(),
                UserName = johnEmail,
                NormalizedUserName = johnEmail.ToUpper(),
                PhoneNumber = "9988776655",
                FirstName = "john",
                LastName = "wick",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            var dean = new User
            {
                Id = deanId,
                Email = deanEmail,
                NormalizedEmail = deanEmail.ToUpper(),
                UserName = deanEmail,
                NormalizedUserName = deanEmail.ToUpper(),
                PhoneNumber = "9988776655",
                FirstName = "dean",
                LastName = "winchester",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            var admin = new User
            {
                Id = adminId,
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                PhoneNumber = "0000000000",
                FirstName = "admin",
                LastName = "",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            //Setting user password
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            john.PasswordHash = passwordHasher.HashPassword(john, "pass@123");
            dean.PasswordHash = passwordHasher.HashPassword(dean, "pass@123");
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin@123");
            builder.Entity<User>().HasData(john, dean, admin);

            //set user role to respective roles
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = customerRoleId,
                UserId = johnId
            },
            new IdentityUserRole<string>
            {
                RoleId = customerRoleId,
                UserId = deanId
            },
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });

            //Setting user address using anonymous type to reference FK
            var johnAddress = new
            {
                Id = Guid.NewGuid(),
                Address = "House no. 12, Green Forest Society",
                City = "New Delhi",
                ZipCode = "110078",
                Country = "India",
                Telephone = "9898767654",
                Mobile = "9876598765",
                UserId = johnId
            };
            var deanAddress = new 
            {
                Id = Guid.NewGuid(),
                Address = "House no. 12, Red Lantern Community",
                City = "Gurugram",
                ZipCode = "110038",
                Country = "India",
                Telephone = "9999767654",
                Mobile = "9888598765",
                UserId = deanId
            };
            builder.Entity<UserAddress>().HasData(johnAddress, deanAddress);

            //Setting user payment using anonymous type to reference FK
            var johnPayment = new 
            {
                Id = Guid.NewGuid(),
                AccountNo = "505050101010",
                PaymentType = "credit card",
                Provider = "gold bank",
                Expiry = new DateTime(2025, 11, 12),
                UserId = johnId
            };
            var deanPayment = new 
            {
                Id = Guid.NewGuid(),
                AccountNo = "606060383838",
                PaymentType = "credit card",
                Provider = "silver bank",
                Expiry = new DateTime(2027, 4, 25),
                UserId = deanId
            };
            builder.Entity<UserPayment>().HasData(johnPayment, deanPayment);
        }
    }
}
