using MemberService.Domain.Entities;

namespace MemberService.Application.Common.Interfaces.Persistence;

public interface IPointRepository
{
    Task<MemberPoint?> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken);
    Task UpdateAsync(MemberPoint memberPoint, CancellationToken cancellationToken);
    Task AddTransactionAsync(PointTransaction transaction, CancellationToken cancellationToken);
}