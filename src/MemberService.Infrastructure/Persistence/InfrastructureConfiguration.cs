using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Infrastructure.Persistence;

public static class InfrastructureConfiguration
{
     public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IPointRepository, PointRepository>();

        return services;
    }
}
