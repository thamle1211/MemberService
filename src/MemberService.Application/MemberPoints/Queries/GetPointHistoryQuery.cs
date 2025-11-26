
using MediatR;
using MemberService.Domain.Entities;

namespace MemberService.Application.MemberPoints.Queries;
public record GetPointHistoryQuery(Guid MemberId) 
    : IRequest<List<PointTransaction>>;
