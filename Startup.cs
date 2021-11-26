using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RocketApi.Models;
using System;
//using RocketApi.Models;

namespace RocketApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      var connectionString = Configuration.GetConnectionString("WebApiDatabase");
      var serverVersion = new MySqlServerVersion(new System.Version(8, 0, 27));
      services.AddDbContext<ApplicationContext>(opt =>
                                         opt.UseMySql(connectionString, serverVersion));
      System.Console.WriteLine(connectionString);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}