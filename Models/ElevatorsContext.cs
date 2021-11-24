using System;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.DbContext;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;


namespace RocketApi.Models
{
    public class ElevatorsContext : DbContext
    {
        //public ElevatorsContext CreateDbContext(string[] args)
        //{

        //}
        public ElevatorsContext(DbContextOptions<ElevatorsContext> options)
            : base(options)
        {
            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
            var mySqlServerVersion = Environment.GetEnvironmentVariable("MYSQL_VERSION");

                var optionsBuilder = new DbContextOptionsBuilder<ElevatorsContext>();
                //var elevatorsContext = new ElevatorsContext();
                // dotnet add package Pomelo.EntityFrameworkCore.MySql
                Console.WriteLine("mySqlServerVersion = ", mySqlServerVersion);
                Console.WriteLine("connectionString = ", connectionString);
                optionsBuilder.UseMySql(
                    connectionString,
                    new MySqlServerVersion(mySqlServerVersion)
                    //options => options.MigrationsAssembly()
                );
        }

        public DbSet<ElevatorItem> ElevatorItems { get; set; }
    }
}