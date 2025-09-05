using Microsoft.EntityFrameworkCore;
using StackBuldTechnicalAssessment.Models;
using StackBuldTechnicalAssessment.Services;

namespace StackBuldTechnicalAssessment.Data
{
    public static class SeedData
    {
        public static void SeedRuntime(ApplicationDbContext context)
        {
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.AddRange(
                    new OrderStatus { StatusName = "Pending" },
                    new OrderStatus { StatusName = "OrderPlaced" },
                    new OrderStatus { StatusName = "Successful" },
                    new OrderStatus { StatusName = "Failed" }
                );
                context.SaveChanges();
            }


            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Laptop", Description = "15-inch laptop", Price = 1200, StockQuantity = 10 },
                    new Product { Name = "Smartphone", Description = "Latest smartphone", Price = 800, StockQuantity = 15 },
                    new Product { Name = "Tablet", Description = "10-inch tablet", Price = 400, StockQuantity = 20 },
                    new Product { Name = "Headphones", Description = "Wireless headphones", Price = 150, StockQuantity = 25 },
                    new Product { Name = "Keyboard", Description = "Mechanical keyboard", Price = 100, StockQuantity = 30 },
                    new Product { Name = "Mouse", Description = "Wireless mouse", Price = 50, StockQuantity = 40 },
                    new Product { Name = "Monitor", Description = "27-inch monitor", Price = 300, StockQuantity = 12 },
                    new Product { Name = "Smartwatch", Description = "Fitness smartwatch", Price = 200, StockQuantity = 18 },
                    new Product { Name = "Camera", Description = "Digital camera", Price = 900, StockQuantity = 7 },
                    new Product { Name = "Printer", Description = "All-in-one printer", Price = 250, StockQuantity = 8 },
                    new Product { Name = "Router", Description = "Wi-Fi 6 router", Price = 180, StockQuantity = 14 },
                    new Product { Name = "External Hard Drive", Description = "1TB USB-C", Price = 120, StockQuantity = 22 },
                    new Product { Name = "Speakers", Description = "Bluetooth speakers", Price = 90, StockQuantity = 16 },
                    new Product { Name = "Webcam", Description = "HD webcam", Price = 70, StockQuantity = 19 },
                    new Product { Name = "Microphone", Description = "USB microphone", Price = 110, StockQuantity = 11 }
                );
                context.SaveChanges();
            }
        }
    }
}
