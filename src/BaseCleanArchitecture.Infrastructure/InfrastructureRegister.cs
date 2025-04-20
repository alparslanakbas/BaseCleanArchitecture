using BaseCleanArchitecture.Infrastructure.Context;
using CD.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Infrastructure
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("PostgreConnection")!;
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            services.Scan(options =>
            {
                options.FromAssemblies(typeof(InfrastructureRegister).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return services;
        }
    }
}
