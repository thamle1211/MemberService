using MediatR;

namespace MemberService.Application.Members.Commands.DeleteMember;
public record DeleteMemberCommand(Guid Id) : IRequest<bool>;
