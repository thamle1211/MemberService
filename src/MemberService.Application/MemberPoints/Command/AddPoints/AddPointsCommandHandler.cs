using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Domain.Entities;

namespace MemberService.Application.MemberPoints.Commands.AddPoints;

public class AddPointsCommandHandler 
    : IRequestHandler<AddPointsCommand, bool>
{
    private readonly IPointRepository _repo;

    public AddPointsCommandHandler(IPointRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(AddPointsCommand request, CancellationToken cancellationToken)
    {
        var point = await _repo.GetByMemberIdAsync(request.MemberId, cancellationToken);

        if (point == null)
            point = new MemberPoint
            {
                Id = Guid.NewGuid(),
                MemberId = request.MemberId,
                Balance = 0
            };

        // Update balance
        point.Balance += request.Points;

        // Create transaction
        var transaction = new PointTransaction
        {
            Id = Guid.NewGuid(),
            MemberId = request.MemberId,
            Amount = request.Points,
            Type = "ADD",
            CreatedAt = DateTime.UtcNow
        };

        await _repo.UpdateAsync(point, cancellationToken);
        await _repo.AddTransactionAsync(transaction, cancellationToken);

        return true;
    }
}
