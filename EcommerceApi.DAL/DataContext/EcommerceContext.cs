using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.DAL.Entities.ProductData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EcommerceApi.DAL.Entities.ShopingCart;

namespace EcommerceApi.DAL.DataContext
{
    public class EcommerceContext : IdentityDbContext<User>
    {
        private readonly Random random = new Random();
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {

        }

        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPayment> UserPayment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductInventory> ProductInventory { get; set; }
        public DbSet<Discount> Discount { get; set; }

        //Shopping Cart Table
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<ShoppingSession> ShoppingSession { get; set; }

        //Seeding data
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

            //ProductData
            //Produc Inventory
            var inventoryList = CreateInventory();
            builder.Entity<ProductInventory>().HasData(inventoryList);

            //Product Category
            var categoryList = CreateCategories();
            builder.Entity<ProductCategory>().HasData(categoryList);


            //Discount
            var discountList = CreateDiscounts();
            var noDiscount = new Discount
            {
                Id = Guid.NewGuid(),
                DiscountPercent = 0d,
                Description = "No Discount",
                Name = "No Discount",
                Active = true,
                CreatedAt = new DateTime(2020, 01, 01),
                ModifiedAt = new DateTime(2020, 01, 01)
            };
            discountList.Add(noDiscount);
            builder.Entity<Discount>().HasData(discountList);

            //Product
            var length = 100; //no. of product
            var productList = new List<object>();
            var productData = new Dictionary<string, List<string>>
            {
                {"Stationary", new List<string> { "Pilot Pen", "Classmate Notebook", "Eraser", "Sharpner", "Ruler",
                    "Marker", "Highlighter", "Stickers", "Watercolors" } },

                {"Electronics", new List<string> { "Smart TV", "Laptop", "Gaming Laptop", 
                    "Smartphone", "Smartwatch", "Earbuds" } },

                {"Furniture", new List<string> {"Chair", "Sofa", "Bed", "Coffee Table", "Nightstand", 
                    "Almirah", "Table", "Desk", "Recliner", "Lawn Chair"} },

                {"Clothes", new List<string> {"T-Shirt", "Shirt", "Pant", "Suit", "Sweatshirt", 
                    "Hoodie", "Cap", "Fedora", "Cargo", "Jeans", "Shorts", "Sweater", "Tuxedo"} },

                {"Footwear", new List<string> {"Sneaker", "Loafer", "Derby", "Boots", "Sandals",
                    "Slippers", "Oxfords", "Slides", "Crocs"} }
            };
            for (int i = 0; i < length; i++)
            {
                var randomCategory = categoryList[random.Next(0, categoryList.Count)];
                var randomDiscount = discountList[random.Next(0, discountList.Count)];
                var randomInventory = inventoryList[random.Next(0, inventoryList.Count)];
                var randomProductName = productData[randomCategory.Name][random.Next(0, productData[randomCategory.Name].Count)];
                var productDate = GetDateBetweenRange(new DateTime(2020, 01, 01), new DateTime(2021, 12, 31));
                productList.Add(new
                {
                    Id = Guid.NewGuid(),
                    Name = randomProductName,
                    ProductCategoryId = randomCategory.Id,
                    Description = "empty",
                    CreatedAt = productDate,
                    ModifiedAt = productDate,
                    DiscountId = (random.Next(1, 100) % 5 == 0 ? randomDiscount.Id : noDiscount.Id),
                    ProductInventoryId = randomInventory.Id,
                    Price = random.Next(3000, 60000) * 1d
                });
            }
            builder.Entity<Product>().HasData(productList);
        }
        private IList<ProductCategory> CreateCategories()
        {
            var categoryNamesDescs = new List<List<string>>();
            categoryNamesDescs.Add(new List<string> { "Stationary", "Books, Notebooks, Pens" });
            categoryNamesDescs.Add(new List<string> { "Electronics", "TVs, Smartphone, Smartwatch, Earbuds" });
            categoryNamesDescs.Add(new List<string> { "Furniture", "Chairs, Beds, Sofas" });
            categoryNamesDescs.Add(new List<string> { "Clothes", "T-shirts, Shirts, Pants, Sweatshirt" });
            categoryNamesDescs.Add(new List<string> { "Footwear", "Boots, Sneakers, Derby" });

            var productCategories = new List<ProductCategory>();
            foreach (var nameDesc in categoryNamesDescs)
            {
                var categoryDate = GetDateBetweenRange(new DateTime(2020, 01, 01), new DateTime(2021, 12, 31));
                productCategories.Add(new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    Name = nameDesc[0],
                    Description = nameDesc[1],
                    CreatedAt = categoryDate,
                    ModifiedAt = categoryDate,
                });
            }
            return productCategories;
        }
        private IList<Discount> CreateDiscounts()
        {
            var discountData = new List<List<string>>();
            discountData.Add(new List<string> { "Diwali", "Diwali Sale", "50" });
            discountData.Add(new List<string> { "Republic", "Republic Day Sale", "30" });
            discountData.Add(new List<string> { "Christmas", "Christmas Sale", "40" });

            var length = discountData.Count; //no. of discount obj
            var discountList = new List<Discount>();
            for (int i = 0; i < length; i++)
            {
                var discountDate = GetDateBetweenRange(new DateTime(2020, 01, 01), new DateTime(2021, 12, 31));
                discountList.Add(new Discount
                {
                    Id = Guid.NewGuid(),
                    Name = discountData[i][0],
                    Description = discountData[i][1],
                    DiscountPercent = double.Parse(discountData[i][2]),
                    CreatedAt = discountDate,
                    ModifiedAt = discountDate,
                    Active = (random.Next(1, 100) % 3 == 0)
                });
            }
            return discountList;
        }
        private IList<ProductInventory> CreateInventory()
        {
            var length = 20; //no. of inventory obj
            var productInventories = new List<ProductInventory>();
            for (int i = 0; i < length; i++)
            {
                var inventoryDate = GetDateBetweenRange(new DateTime(2020, 01, 01), new DateTime(2021, 12, 31));
                productInventories.Add(new ProductInventory
                {
                    Id = Guid.NewGuid(),
                    Quantity = random.Next(100, 3000),
                    CreatedAt = inventoryDate,
                    ModifiedAt = inventoryDate
                });
            }
            return productInventories;
        }
        private DateTime GetDateBetweenRange(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
