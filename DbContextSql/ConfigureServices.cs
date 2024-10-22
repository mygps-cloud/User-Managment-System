// using Ipstatuschecker.DbContextSql;
// using Microsoft.EntityFrameworkCore;

// namespace Ipstatuschecker.DbContext
// {



//      public class ConfigureServicesDB
//     {
//         private static string dbconnect =  "Server=localhost;Database=test;User=root;Password=password;Min Pool Size=10;";

//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddDbContext<IpCheck>(options =>
//                 options.UseMySql(dbconnect, ServerVersion.AutoDetect(dbconnect)));

//                 // services.AddScoped<IProductService, ProductService>();
//         }
//     }
// }

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ipstatuschecker.DbContextSql; 

namespace Ipstatuschecker.DbContext
{
    public class ConfigureServicesDB
    {
        private static string dbconnect = "Data Source=UserIpChecker.db"; 

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IpCheck>(options =>
                options.UseSqlite(dbconnect)); 
            // რეგისტრირება სხვა სერვისებისთვის
            // services.AddScoped<IProductService, ProductService>();
        }
    }
}

