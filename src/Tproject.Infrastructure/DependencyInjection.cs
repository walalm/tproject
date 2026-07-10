using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tproject.Domain.Enums;
using Tproject.Infrastructure.Data;
using Tproject.Infrastructure.Services;

namespace Tproject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var providerName = configuration["Database:Provider"] ?? "InMemory";
        var provider = Enum.TryParse<DatabaseProvider>(providerName, ignoreCase: true, out var parsed)
            ? parsed
            : DatabaseProvider.InMemory;

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            switch (provider)
            {
                case DatabaseProvider.SqlServer:
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new InvalidOperationException(
                            "Connection string 'DefaultConnection' is required when using SqlServer provider.");
                    }
                    options.UseSqlServer(connectionString);
                    break;
                case DatabaseProvider.InMemory:
                    options.UseInMemoryDatabase("TprojectDb");
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported database provider: {provider}");
            }
        });

        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}
