using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Domain.Entities;

namespace MemberService.Application.MemberPoints.Queries;

public class GetPointHistoryQueryHandler 
    : IRequestHandler<GetPointHistoryQuery, List<PointTransaction>>
{
    private readonly IPointRepository _repo;

    public GetPointHistoryQueryHandler(IPointRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<PointTransaction>> Handle(GetPointHistoryQuery request, CancellationToken cancellationToken)
    {
        var point = await _repo.GetByMemberIdAsync(request.MemberId, cancellationToken);
        return point?.Transactions.OrderByDescending(x => x.CreatedAt).ToList() 
               ?? new List<PointTransaction>();
    }
}
