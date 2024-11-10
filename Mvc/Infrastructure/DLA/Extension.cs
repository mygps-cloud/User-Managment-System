using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Ipstatuschecker.Mvc.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Mvc.Infrastructure.Persistence;

namespace Ipstatuschecker.Mvc.Infrastructure.DLA
{
    internal static class Extension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection serviceDescriptors)
        {


            string dbconnect = "Server=localhost;Port=3306;Database=UserManagment;User Id=root;Password=password;";

            serviceDescriptors.AddDbContext<DbIpCheck>
            (options =>options.UseMySql
            (dbconnect, ServerVersion.AutoDetect(dbconnect)),
             ServiceLifetime.Scoped);


            serviceDescriptors.AddScoped<IQueryUserRepository<User>, UserQueryRepository>();
            serviceDescriptors.AddScoped<ICommandUserRepository<User>, UserCommandIRepository>();
            serviceDescriptors.AddScoped<IUserservices<UserDto>, ServiceUser>();
            serviceDescriptors.AddSingleton<UserStatisticServices>();



            return serviceDescriptors;

        }
    }
}