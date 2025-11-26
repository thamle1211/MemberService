using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Domain.Entities;
using MemberService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MemberService.Infrastructure.Persistence.Repositories;

public class PointRepository : IPointRepository
{
    private readonly AppDbContext _db;

    public PointRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<MemberPoint?> GetByMemberIdAsync(
        Guid memberId, 
        CancellationToken cancellationToken)
    {
        return await _db.MemberPoints
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.MemberId == memberId, cancellationToken);
    }

    public async Task UpdateAsync(
        MemberPoint memberPoint, 
        CancellationToken cancellationToken)
    {
        _db.MemberPoints.Update(memberPoint);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task AddTransactionAsync(
        PointTransaction transaction, 
        CancellationToken cancellationToken)
    {
        await _db.PointTransactions.AddAsync(transaction, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}