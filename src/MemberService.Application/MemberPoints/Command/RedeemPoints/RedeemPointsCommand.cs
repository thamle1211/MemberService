using MediatR;

namespace MemberService.Application.MemberPoints.Commands.RedeemPoints;
public record RedeemPointsCommand(Guid MemberId, int Points) : IRequest<bool>;