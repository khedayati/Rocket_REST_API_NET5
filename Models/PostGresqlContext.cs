using System;
using Microsoft.EntityFrameworkCore;
using RocketApi.Models;

namespace RocketApi.Models
{
    public class PostGresqlContext : DbContext
    {
        public PostGresqlContext(DbContextOptions<PostGresqlContext> options)
            : base(options)
        { }
        // public DbSet<RocketApi.Models.fact_interventions> fact_interventions { get; set; }

        public DbSet<RocketApi.Models.fact_interventions> fact_interventions { get; set; }
        public DbSet<RocketApi.Models.question> question { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<fact_interventions>().HasKey(x => x.intervention_id);
            modelBuilder.Entity<question>().HasNoKey();
            base.OnModelCreating(modelBuilder);


        }
        
    }
}