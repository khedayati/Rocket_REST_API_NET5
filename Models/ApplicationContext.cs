using System;
using Microsoft.EntityFrameworkCore;
using RocketApi.Models;

namespace RocketApi.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {}

        public DbSet<Elevator> elevators { get; set; }

        public DbSet<RocketApi.Models.addresses> addresses { get; set; }
    }
}