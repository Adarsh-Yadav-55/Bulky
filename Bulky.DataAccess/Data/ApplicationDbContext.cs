using Bulky.Models;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Atomic Habits",
                    Description = "An Easy & Proven Way to Build Good Habits & Break Bad Ones.",
                    ISBN = "9780735211292",
                    Author = "James Clear",
                    ListPrice = 29.99,
                    Price = 24.99,
                    Price50 = 22.99,
                    Price100 = 20.99,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Description = "Your Journey to Mastery",
                    ISBN = "9780135957059",
                    Author = "Andrew Hunt, David Thomas",
                    ListPrice = 59.99,
                    Price = 54.99,
                    Price50 = 52.99,
                    Price100 = 49.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Clean Code",
                    Description = "A Handbook of Agile Software Craftsmanship",
                    ISBN = "9780132350884",
                    Author = "Robert C. Martin",
                    ListPrice = 49.99,
                    Price = 44.99,
                    Price50 = 42.99,
                    Price100 = 39.99,
                    CategoryId = 1,
                    ImageUrl=""
                });
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "TechNova Solutions",
                    StreetAddress = "123 Innovation Drive",
                    City = "Seattle",
                    State = "WA",
                    PostalCode = "98101",
                    PhoneNumber = "206-555-0123"
                },
                new Company
                {
                    Id = 2,
                    Name = "GreenLeaf Industries",
                    StreetAddress = "456 Eco Park Blvd",
                    City = "Portland",
                    State = "OR",
                    PostalCode = "97205",
                    PhoneNumber = "503-555-0198"
                },
                new Company
                {
                    Id = 3,
                    Name = "Skyline Logistics",
                    StreetAddress = "789 Cargo Way",
                    City = "Denver",
                    State = "CO",
                    PostalCode = "80202",
                    PhoneNumber = "303-555-0175"
                }
            );
        }
    }
}
