using System;
using Microsoft.EntityFrameworkCore;

namespace RocketApi.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {}

        public DbSet<Elevators> elevators { get; set; }
        public DbSet<Elevators> Columns { get; set; }
        public DbSet<Elevators> Buildings { get; set; }
    }
}