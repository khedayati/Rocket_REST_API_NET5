using System;
using Microsoft.EntityFrameworkCore;

namespace RocketApi.Models
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    public DbSet<Elevator> elevators { get; set; }
    public DbSet<Lead> leads { get; set; }
    public DbSet<Battery> batteries { get; set; }
    public DbSet<Building> buildings { get; set; }

    public DbSet<BuildingDetail> buildingdetails { get; set; }
    public DbSet<Column> columns { get; set; }
    public DbSet<Customer> customers { get; set; }

  }
}