using MediatR;
using MemberService.Application.Common.Exceptions;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Domain.Entities;

namespace MemberService.Application.MemberPoints.Commands.RedeemPoints;

public class RedeemPointsCommandHandler 
    : IRequestHandler<RedeemPointsCommand, bool>
{
    private readonly IPointRepository _repo;

    public RedeemPointsCommandHandler(IPointRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(RedeemPointsCommand request, CancellationToken cancellationToken)
    {
        var point = await _repo.GetByMemberIdAsync(request.MemberId, cancellationToken);
        if (point == null)
            throw new NotFoundException($"Point account for member {request.MemberId} not found.");

        if (point.Balance < request.Points)
            throw new BusinessRuleException("Not enough points to redeem.");

        point.Balance -= request.Points;

        var transaction = new PointTransaction
        {
            Id = Guid.NewGuid(),
            MemberId = request.MemberId,
            Amount = -request.Points,
            Type = "REDEEM",
            CreatedAt = DateTime.UtcNow
        };

        await _repo.UpdateAsync(point, cancellationToken);
        await _repo.AddTransactionAsync(transaction, cancellationToken);

        return true;
    }
}
