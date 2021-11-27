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

        public DbSet<Addresses> addresses { get; set; }

        public DbSet<Column> columns { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Customer> customers { get; set; }

        public DbSet<Lead> leads { get; set; }
        public DbSet<buildings> buildings { get; set; }

        public DbSet<BuildingDetails> building_details { get; set; }


        //public DbSet<Lead> leads { get; set; }
        //public DbSet<RocketApi.Models.Building> Building { get; set; }

    }
}
