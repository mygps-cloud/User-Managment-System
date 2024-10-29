

using Microsoft.EntityFrameworkCore;


namespace Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql
{

   
    public class ConfigureServicesDB
    {
        private static string dbconnect = "Data Source=UserIpChecker.db"; 

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbIpCheck>(options =>
                options.UseSqlite(dbconnect)); 
            // რეგისტრირება სხვა სერვისებისთვის
            // services.AddScoped<IProductService, ProductService>();
        }
    }
}

