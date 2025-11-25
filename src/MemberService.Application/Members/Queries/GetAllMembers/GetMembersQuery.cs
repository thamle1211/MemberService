using MediatR;
using MemberService.Application.Members.Common;

namespace MemberService.Application.Members.Queries.GetAllMembers;
public record GetMembersQuery : IRequest<List<MemberDto>>;
