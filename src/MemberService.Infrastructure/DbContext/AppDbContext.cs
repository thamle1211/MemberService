using Microsoft.EntityFrameworkCore;
using MemberService.Domain.Entities;

namespace MemberService.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
}
