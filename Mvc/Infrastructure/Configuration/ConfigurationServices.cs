
using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Ipstatuschecker.Mvc.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Mvc.Infrastructure.Persistence;

namespace Ipstatuschecker.Mvc.Infrastructure.Configuration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection serviceDescriptors)
        {
         string dbconnect = "Data Source=UserIpChecker.db"; 
         
         serviceDescriptors.AddDbContext<DbIpCheck>(options => options.UseSqlite(dbconnect)); 
         serviceDescriptors.AddScoped<IQueryIpStatusRepository<User>,UserQueryRepository>();
         serviceDescriptors.AddScoped<ICommandIpStatusRepository<User>,UserCommandIRepository>();
         serviceDescriptors.AddScoped<IUserservices<UserDto>, ServiceUser>();
         return serviceDescriptors;

        }
      
    }
}