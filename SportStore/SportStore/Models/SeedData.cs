using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicatioDbContext context = app.ApplicationServices.GetService(typeof(ApplicatioDbContext)) as ApplicatioDbContext;
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    Category = "Watersports",
                    Price = 275},
                new Product {
                    Name = "LifeJacket",
                    Description = "Protective and nice",
                    Category = "Watersports",
                    Price = 48.95m},
                new Product
                {
                    Name = "Ball",
                    Description = "FIFA-approved",
                    Category = "Football",
                    Price = 18.95m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Cool and red",
                    Category = "Football",
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadium",
                    Description = "35000 seats Stadium",
                    Category = "Football",
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "+75% to efficiency",
                    Category = "Chess",
                    Price = 14.95m
                },
                new Product
                {
                    Name = "Unsteady Chair",
                    Description = "-35% to your opponent adventage",
                    Category = "Chess",
                    Price = 24.95m
                },
                new Product
                {
                    Name = "Human Chess Borad",
                    Description = "A fun game for the family",
                    Category = "Chess",
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated king",
                    Category = "Chess",
                    Price = 1200
                }
                );
                context.SaveChanges();
            }
        }
    }
}
