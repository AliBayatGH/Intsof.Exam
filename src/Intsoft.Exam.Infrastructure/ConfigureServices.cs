using System.Reflection;
using Intsoft.Exam.Application.Contracts;
using Intsoft.Exam.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Intsoft.Exam.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserManagerService, UserManagerService>();

            return services;
        }
    }
}
