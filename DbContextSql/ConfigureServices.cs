using Ipstatuschecker.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.DbContext
{
  

        
     public class ConfigureServicesDB
    {
        private static string dbconnect =  "Server=localhost;Database=test;User=root;Password=password;Min Pool Size=10;";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IpCheck>(options =>
                options.UseMySql(dbconnect, ServerVersion.AutoDetect(dbconnect)));

                // services.AddScoped<IProductService, ProductService>();
        }
    }
}