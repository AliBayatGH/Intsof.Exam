using Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance;
public static class DependencyInjection
{
    public static IServiceCollection AddPersisntace(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TestContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("Local")!));

        services.AddScoped<ITestContext, TestContext>();
        return services;
    }

}
