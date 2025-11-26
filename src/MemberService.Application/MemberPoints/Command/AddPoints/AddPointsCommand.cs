using MediatR;

namespace MemberService.Application.MemberPoints.Commands.AddPoints;

public record AddPointsCommand(Guid MemberId, int Points) : IRequest<bool>;
