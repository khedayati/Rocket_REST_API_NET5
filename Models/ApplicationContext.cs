using System;
using Microsoft.EntityFrameworkCore;

namespace RocketApi.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {}

        public DbSet<Elevator> elevators { get; set; }
    }
}