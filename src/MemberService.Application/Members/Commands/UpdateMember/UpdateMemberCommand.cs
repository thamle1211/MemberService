using MediatR;

namespace MemberService.Application.Members.Commands.UpdateMember;
public record UpdateMemberCommand(Guid Id, string FirstName, string LastName, string Email, DateTime BirthDate)
    : IRequest<bool>;
