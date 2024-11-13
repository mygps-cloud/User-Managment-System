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


<<<<<<< HEAD
            string dbconnect = "Server=localhost;Port=3306;Database=main;User Id=root;Password=password;";
=======
            string dbconnect = "Server=localhost;Port=3306;Database=Feature;User Id=root;Password=password;";
>>>>>>> 0ca690d1fa1d7b343f7d25090cfa00f4223ead2e

            serviceDescriptors.AddDbContext<DbIpCheck>
            (options =>options.UseMySql
            (dbconnect, ServerVersion.AutoDetect(dbconnect)));


            serviceDescriptors.AddScoped<IQueryUserRepository<User>, UserQueryRepository>();
            serviceDescriptors.AddScoped<ICommandUserRepository<User>, UserCommandIRepository>();
            serviceDescriptors.AddScoped<IUserservices<UserDto>, ServiceUser>();
            serviceDescriptors.AddSingleton<UserStatisticServices>();



            return serviceDescriptors;

        }
    }
}