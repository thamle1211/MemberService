using MediatR;
using MemberService.Application.Members.Model;

namespace MemberService.Application.Members.Queries.GetAllMembers;
public record GetMembersQuery : IRequest<List<MemberDto>>;
