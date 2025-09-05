using Microsoft.EntityFrameworkCore;
using StackBuldTechnicalAssessment.Data;
using StackBuldTechnicalAssessment.Models;
using System;

namespace StackBuldTechnicalAssessment.Services
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

    }
}