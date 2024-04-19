using Domain.Abstractions;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var postegreConnection = configuration.GetConnectionString("postegreDatabase");

            services.AddDbContext<AppDbContext>(opt =>
                             opt.UseNpgsql(postegreConnection)
                             );

            services.AddScoped<IUsuarioRepositoty, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var myHandlers = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

            return services;
        }
    }
}
