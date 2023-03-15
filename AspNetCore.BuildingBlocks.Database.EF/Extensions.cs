using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCore.BuildingBlocks.Database.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddAndConfigureCreateDbContext<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionAction = null,
            Action<SqlServerDbContextOptionsBuilder> dbContextOptionAction = null)
            where TContext : DbContext
        {
            return services.AddAndConfigureDbContext<TContext>(DatabaseType.Create, optionAction);
        }

        public static IServiceCollection AddAndConfigureMarketDbContext<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionAction = null,
            Action<SqlServerDbContextOptionsBuilder> dbContextOptionAction = null)
            where TContext : DbContext
        {
            return services.AddAndConfigureDbContext<TContext>(DatabaseType.MktData, optionAction);
        }

        public static IServiceCollection AddAndConfigureDbContext<TContext>(
            this IServiceCollection services,
            DatabaseType dbType,
            Action<DbContextOptionsBuilder> optionAction = null,
            Action<SqlServerDbContextOptionsBuilder> dbContextOptionAction = null)
            where TContext : DbContext
        {
            IConnectionStringProvider connectionStringProvider;
            IHostingEnvironment hostingEnv;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                connectionStringProvider = serviceProvider.GetService<IConnectionStringProvider>();
                hostingEnv = serviceProvider.GetService<IHostingEnvironment>();
            }

            var connectionString = connectionStringProvider.GetConnectionString(dbType);

            void CongureOptions(DbContextOptionsBuilder opt)
            {
                optionAction?.Invoke(opt);
                opt
                    .UseSqlServer(connectionString, dbContextOptionAction)
                    .EnableDetailedErrors(hostingEnv.IsDevelopment());
            }

            services.AddDbContext<TContext>(CongureOptions);
            return services;
        }
    }
}