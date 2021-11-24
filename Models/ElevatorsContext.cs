using System;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.DbContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;



namespace RocketApi.Models
{
    public class ElevatorsContext : DbContext
    {
        //public ElevatorsContext CreateDbContext(string[] args)
        //{

        //}
        //private readonly IConfiguration _configuration;
        //public ElevatorsItemsController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //private readonly ServiceSettings settings;
        public ElevatorsContext(DbContextOptions<ElevatorsContext> options)
            : base(options)
        {
            /*
            var connectionString = System.Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
            var mySqlServerVersion = System.Environment.GetEnvironmentVariable("MYSQL_VERSION");
            //var connectionString = ConfigurationPath.GetSectionKey("MYSQL_CONNECTION_STRING");
            Console.WriteLine("mySqlServerVersion = ", mySqlServerVersion);
            Console.WriteLine("connectionString = ", connectionString);

            if (!string.IsNullOrEmpty(connectionString)) {
                Console.WriteLine("STRING IS EMPTY");
                //connectionString = Configuration.GetConnectionString("mySqlConnection");
                //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseMySQL(connectionString));
            } else {
                //connectionString = Configuration.GetConnectionString("DefaultConnection");
                //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(connectionString));
            }
            */
            var connectionString = "host=localhost;user=root;password=;database=relational_database;";
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //var optionsBuilder = new DbContextOptionsBuilder<ElevatorsContext>();
            var optionsBuilder = new DbContextOptionsBuilder<ElevatorsContext>();
            
            //var elevatorsContext = new ElevatorsContext();
            // dotnet add package Pomelo.EntityFrameworkCore.MySql
                
            optionsBuilder.UseMySql(
                connectionString,
                //.UseMySQL(connectionString, mySqlServerVersion),
                new MySqlServerVersion(mySqlServerVersion)
                //options => options.MigrationsAssembly()
            );
        }

        public DbSet<ElevatorItem> ElevatorItems { get; set; }
    }
}