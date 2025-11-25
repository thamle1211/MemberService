using MediatR;
using MemberService.Application.Common.Interfaces;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Domain.Entities;

namespace MemberService.Application.Members.Commands.CreateMember;
public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _repo;
    private readonly IOperatorService _operatorService;
    
    public CreateMemberCommandHandler(IMemberRepository repo, IOperatorService operatorService)
    {
        _repo = repo;
        _operatorService = operatorService;
    }

    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken ct)
    {
        var member = new Member(request.FirstName, request.LastName, request.Email, request.BirthDate, _operatorService.UserId);
        await _repo.AddAsync(member);
        return member.Id;
    }
}
