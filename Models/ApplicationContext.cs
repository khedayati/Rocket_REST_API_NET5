using System;
using Microsoft.EntityFrameworkCore;



namespace RocketApi.Models
{
    public class ApplicationContext : DbContext
    {
        //protected readonly IConfiguration Configuration;

        //public ElevatorsContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
            // connect to mysql with connection string from app settings
            //var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //}
        //public ElevatorsContext CreateDbContext(string[] args)
        //{

        //}
        //private readonly IConfiguration _configuration;
        //public ElevatorsItemsController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //private readonly ServiceSettings settings;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //var connectionString = Configuration.GetConnectionString("WebApiDatabase");
           // var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            
            //var connectionString = System.Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
            //var mySqlServerVersion = System.Environment.GetEnvironmentVariable("MYSQL_VERSION");
            //var connectionString = ConfigurationPath.GetSectionKey("MYSQL_CONNECTION_STRING");
            //var connectionString = "host=localhost;user=root;password=;database=relational_database;";
            //Console.WriteLine("mySqlServerVersion = ", mySqlServerVersion);
            //Console.WriteLine("connectionString = ", connectionString);

            //if (!string.IsNullOrEmpty(connectionString)) {
            //    Console.WriteLine("STRING IS EMPTY");
                //connectionString = Configuration.GetConnectionString("mySqlConnection");
                //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseMySQL(connectionString));
            //} else {
                //connectionString = Configuration.GetConnectionString("DefaultConnection");
                //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(connectionString));
           // }
            
            //var connectionString = "host=localhost;user=root;password=;database=relational_database;";
            //var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //var optionsBuilder = new DbContextOptionsBuilder<ElevatorsContext>();
           // var optionsBuilder = new DbContextOptionsBuilder<ElevatorsContext>();
            
            //var elevatorsContext = new ElevatorsContext();
            // dotnet add package Pomelo.EntityFrameworkCore.MySql
                
           // optionsBuilder.UseMySql(
            //    connectionString,
                //.UseMySQL(connectionString, mySqlServerVersion),
           //     new MySqlServerVersion(mySqlServerVersion)
                //options => options.MigrationsAssembly()
           // );
        }

        public DbSet<Elevator> elevators { get; set; }
    //public DbSet<ElevatorItem> BatteryItems { get; set; }
    }
}